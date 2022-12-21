using Edu.WebApi.Application.Identity.Users;
using Edu.WebApi.Application.Interfaces.Chat;
using Edu.WebApi.Application.Models.Chat;

namespace Edu.WebApi.Application.Interfaces.Services;
public interface IChatService
{
    Task<IEnumerable<ChatUserResponse>> GetChatUsersAsync(string userId);

    Task SaveMessageAsync(ChatHistory<IChatUser> message);

    Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string userId, string contactId);
}
