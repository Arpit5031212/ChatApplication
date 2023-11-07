using ChatApp.Context.EntityClasses;
using ChatApp.Models;

namespace ChatApp.Business.ServiceInterfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatById(int id);
        List<RecentChatsOfUser> GetAllRecentChatsOfUser(int id);
        Task<List<Chat>> GetAllChatsBetweenTwoContacts(int SenderId, int RecieverId);
        Task<List<Chat>> GetAllChatsOfUser(int id);
        Task<Chat> SendChat(ChatViewModel chat);
        Task<Chat> ReplyToChat(ChatViewModel chat);
        Task<Chat> EditChat(int id, ChatViewModel chat);
        Task<Chat> DeleteChat(int id);
    }
}
