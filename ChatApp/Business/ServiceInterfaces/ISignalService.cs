using ChatApp.Context.EntityClasses;

namespace ChatApp.Business.ServiceInterfaces
{
    public interface ISignalService
    {
        Task AddOnlineUser(UserConnection userConnection);
        Task RemoveUserConnection(UserConnection userConnection);
        Task<UserConnection> FetchConnectedUser(string searchTerm);
    }
}
