using ChatApp.Context.EntityClasses;
using ChatApp.Models;

namespace ChatApp.Business.ServiceInterfaces
{
    public interface IGeneralService
    {
        public Task<List<SearchResult>> SearchUser(string searchQuery, int currentUserId);
        public Task<SearchedUser> GetUserById(int id);
    }
}
