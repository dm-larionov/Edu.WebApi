using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Edu.WebApi.Application.Common.Exceptions;
using Edu.WebApi.Application.Identity.Tokens;
using Edu.WebApi.Application.Identity.Users;
using Edu.WebApi.Infrastructure.Auth;
using Edu.WebApi.Infrastructure.Auth.Jwt;
using Edu.WebApi.Shared.Authorization;
using Edu.WebApi.Shared.Multitenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Edu.WebApi.Infrastructure.Identity;

internal class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IStringLocalizer _t;
    private readonly SecuritySettings _securitySettings;
    private readonly JwtSettings _jwtSettings;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<JwtSettings> jwtSettings,
        IStringLocalizer<TokenService> localizer,
        IOptions<SecuritySettings> securitySettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _t = localizer;
        _jwtSettings = jwtSettings.Value;
        _securitySettings = securitySettings.Value;
    }

    public async Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email.Trim().Normalize()) is not { } user
            || !await _userManager.CheckPasswordAsync(user, request.Password))
        {

            throw new UnauthorizedException(_t["Authentication Failed."]);
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedException(_t["User Not Active. Please contact the administrator."]);
        }

        if (_securitySettings.RequireConfirmedAccount && !user.EmailConfirmed)
        {
            throw new UnauthorizedException(_t["E-Mail not confirmed."]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        string? userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            throw new UnauthorizedException(_t["Authentication Failed."]);
        }

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedException(_t["Invalid Refresh Token."]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress)
    {
        string token = await GenerateJwt(user, ipAddress);

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

        await _userManager.UpdateAsync(user);

        return new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
    }

    public async Task<List<string>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<string>();

        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken);
        foreach (var role in roles)
        {
            if (await _userManager.IsInRoleAsync(user, role.Name))
                userRoles.Add(role.Name);
        }

        return userRoles;
    }

    private async Task<string> GenerateJwt(ApplicationUser user, string ipAddress) =>
        GenerateEncryptedToken(GetSigningCredentials(), await GetClaims(user, ipAddress));

    private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user, string ipAddress) =>
        new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(EduClaims.Fullname, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Name, user.FirstName ?? string.Empty),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new(EduClaims.IpAddress, ipAddress),
            new(EduClaims.ImageUrl, user.ImageUrl ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
        }.Union((await GetRolesAsync(user.Id, CancellationToken.None)).Select(x =>
            new Claim(ClaimTypes.Role, x)).ToList());

    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new UnauthorizedException(_t["Invalid Token."]);
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
}