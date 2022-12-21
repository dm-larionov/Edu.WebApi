using Edu.WebApi.Application.Identity.Users;
using Edu.WebApi.Application.Interfaces.Chat;
using Edu.WebApi.Application.Interfaces.Services;
using Edu.WebApi.Application.Models.Chat;
using Mapster;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Edu.WebApi.Infrastructure.Services
{
    /*
    public class ChatService : IChatService
    {
        private readonly IdentityDbContext _context;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<ChatService> _localizer;

        public ChatService(
            IdentityDbContext context,
            IUserService userService,
            IStringLocalizer<ChatService> localizer)
        {
            _context = context;
            _userService = userService;
            _localizer = localizer;
        }

        public async Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string userId, string contactId)
        {
            var response = await _userService.GetAsync(userId);

            var user = response;
            var query = await _context.ChatHistories
                .Where(h => (h.FromUserId == userId && h.ToUserId == contactId) || (h.FromUserId == contactId && h.ToUserId == userId))
                .OrderBy(a => a.CreatedDate)
                .Include(a => a.FromUser)
                .Include(a => a.ToUser)
                .Select(x => new ChatHistoryResponse
                {
                    FromUserId = x.FromUserId,
                    FromUserFullName = $"{x.FromUser.FirstName} {x.FromUser.LastName}",
                    Message = x.Message,
                    CreatedDate = x.CreatedDate,
                    Id = x.Id,
                    ToUserId = x.ToUserId,
                    ToUserFullName = $"{x.ToUser.FirstName} {x.ToUser.LastName}",
                    ToUserImageURL = x.ToUser.ProfilePictureDataUrl,
                    FromUserImageURL = x.FromUser.ProfilePictureDataUrl
                }).ToListAsync();
            return query;

        }

        public async Task<IEnumerable<ChatUserResponse>> GetChatUsersAsync(string userId)
        {
            var userRoles = await _userService.GetRolesAsync(userId);
            var userIsAdmin = userRoles.Data?.UserRoles?.Any(x => x.Selected && x.RoleName == RoleConstants.AdministratorRole) == true;
            var allUsers = await _context.Users.Where(user => user.Id != userId && (userIsAdmin || user.IsActive && user.EmailConfirmed)).ToListAsync();
            var chatUsers = allUsers.Adapt<IEnumerable<ChatUserResponse>>();
            return chatUsers;
        }

        public async Task SaveMessageAsync(ChatHistory<IChatUser> message)
        {
            message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
            await _context.ChatHistories.AddAsync(message.Adapt<ChatHistory<BlazorUser>>());
            await _context.SaveChangesAsync();
        }
    }
    */
}