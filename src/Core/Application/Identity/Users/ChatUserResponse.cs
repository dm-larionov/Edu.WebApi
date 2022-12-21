using Edu.WebApi.Application.Interfaces.Chat;
using Edu.WebApi.Application.Models.Chat;

namespace Edu.WebApi.Application.Identity.Users;

public class ChatUserResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string ProfilePictureDataUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public bool IsOnline { get; set; }
    public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryFromUsers { get; set; }
    public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryToUsers { get; set; }
}
