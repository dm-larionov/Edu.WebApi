using Edu.WebApi.Infrastructure.Identity;
using Edu.WebApi.Infrastructure.Persistence.Context;
using Edu.WebApi.Shared.Authorization;
using Edu.WebApi.Shared.Multitenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Edu.WebApi.Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    //private readonly EduTenantInfo _currentTenant;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(/*EduTenantInfo currentTenant,*/ RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        //_currentTenant = currentTenant;
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await SeedHeadEduAsync();
        await SeedEmployeeEduAsync();
        await SeedTeacherAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext)
    {
        foreach (string roleName in EduRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
            {
                // Create the role
                _logger.LogInformation("Seeding {role} Role for Tenant.", roleName);
                role = new ApplicationRole(roleName, $"{roleName} Role for Tenant");
                await _roleManager.CreateAsync(role);
            }

            // Assign permissions
            if (roleName == EduRoles.Basic)
            {
                await AssignPermissionsToRoleAsync(dbContext, EduPermissions.Basic, role);
            }
            if (roleName == EduRoles.Admin)
            {
                //await AssignPermissionsToRoleAsync(dbContext, EduPermissions.Admin, role);
                await AssignPermissionsToRoleAsync(dbContext, EduPermissions.All, role);
                //await AssignPermissionsToRoleAsync(dbContext, EduPermissions.Root, role);
            }
            if (roleName == EduRoles.HeadUmo)
                await AssignPermissionsToRoleAsync(dbContext, EduPermissions.EDM_Head, role);
            if (roleName == EduRoles.EmployeeUmo)
                await AssignPermissionsToRoleAsync(dbContext, EduPermissions.EDM_Employee, role);
            if (roleName == EduRoles.Teacher)
                await AssignPermissionsToRoleAsync(dbContext, EduPermissions.Teacher, role);
        }
    }

    private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<EduPermission> permissions, ApplicationRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(c => c.Type == EduClaims.Permission && c.Value == permission.Name))
            {
                _logger.LogInformation("Seeding {role} Permission '{permission}' for Tenant.", role.Name, permission.Name);
                dbContext.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = EduClaims.Permission,
                    ClaimValue = permission.Name,
                    CreatedBy = "ApplicationDbSeeder"
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == "admin@root.com")
            is not ApplicationUser adminUser)
        {
            string adminUserName = $"{"root"}.{EduRoles.Admin}".ToLowerInvariant();
            adminUser = new ApplicationUser
            {
                FirstName = "root",
                LastName = EduRoles.Admin,
                Email = "admin@root.com",
                UserName = adminUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "admin@root.com".ToUpperInvariant(),
                NormalizedUserName = adminUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Admin User for Tenant.");
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, MultitenancyConstants.DefaultPassword);
            await _userManager.CreateAsync(adminUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(adminUser, EduRoles.Admin))
        {
            _logger.LogInformation("Assigning Admin Role to Admin User for Tenant.");
            await _userManager.AddToRoleAsync(adminUser, EduRoles.Admin);
        }
    }

    private async Task SeedHeadEduAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == "head_edu@root.com")
            is not ApplicationUser headEduUser)
        {
            string headEduUserName = $"{"EDM"}.{"Head"}".ToLowerInvariant();
            headEduUser = new ApplicationUser
            {
                FirstName = "Руководитель",
                LastName = "Умо",
                Email = "head_edu@root.com",
                UserName = headEduUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "head_edu@root.com".ToUpperInvariant(),
                NormalizedUserName = headEduUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Head Edu User for Tenant.");
            var password = new PasswordHasher<ApplicationUser>();
            headEduUser.PasswordHash = password.HashPassword(headEduUser, MultitenancyConstants.DefaultPassword);
            await _userManager.CreateAsync(headEduUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(headEduUser, EduRoles.HeadUmo))
        {
            _logger.LogInformation("Assigning Head Umo Role to Head Edu User for Tenant.");
            await _userManager.AddToRoleAsync(headEduUser, EduRoles.HeadUmo);
        }
    }

    private async Task SeedEmployeeEduAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == "employee_edu@root.com")
            is not ApplicationUser employeeEduUser)
        {
            string employeeEduUserName = $"{"EDM"}.{"Employee"}".ToLowerInvariant();
            employeeEduUser = new ApplicationUser
            {
                FirstName = "Сотрудник",
                LastName = "Умо",
                Email = "employee_edu@root.com",
                UserName = employeeEduUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "employee_edu@root.com".ToUpperInvariant(),
                NormalizedUserName = employeeEduUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Employee Edu User for Tenant.");
            var password = new PasswordHasher<ApplicationUser>();
            employeeEduUser.PasswordHash = password.HashPassword(employeeEduUser, MultitenancyConstants.DefaultPassword);
            await _userManager.CreateAsync(employeeEduUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(employeeEduUser, EduRoles.EmployeeUmo))
        {
            _logger.LogInformation("Assigning Head Umo Role to Head Edu User for Tenant.");
            await _userManager.AddToRoleAsync(employeeEduUser, EduRoles.EmployeeUmo);
        }
    }

    private async Task SeedTeacherAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == "teacher@root.com")
            is not ApplicationUser teacherUser)
        {
            string teacherUserName = $"{"Teacher"}".ToLowerInvariant();
            teacherUser = new ApplicationUser
            {
                FirstName = "Преподаватель",
                LastName = "Умо",
                Email = "teacher@root.com",
                UserName = teacherUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "teacher@root.com".ToUpperInvariant(),
                NormalizedUserName = teacherUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Teacher User for Tenant.");
            var password = new PasswordHasher<ApplicationUser>();
            teacherUser.PasswordHash = password.HashPassword(teacherUser, MultitenancyConstants.DefaultPassword);
            await _userManager.CreateAsync(teacherUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(teacherUser, EduRoles.Teacher))
        {
            _logger.LogInformation("Assigning Teacher Role to Teacher User for Tenant.");
            await _userManager.AddToRoleAsync(teacherUser, EduRoles.Teacher);
        }
    }
}