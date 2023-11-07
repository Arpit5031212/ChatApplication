using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure
{
    public class SignalService : ISignalService
    {
        private readonly ChatAppContext _chatAppContext;
        public SignalService( ChatAppContext chatAppContext ) 
        { 
            _chatAppContext = chatAppContext;
        }

        public async Task AddOnlineUser(UserConnection userConnection)
        {
            IEnumerable<UserConnection> users = await _chatAppContext.UserConnections.Where(x => x.UserId == userConnection.UserId).ToListAsync();
            if (users.Any())
            {
                this._chatAppContext.UserConnections.RemoveRange(users);
            }

            try
            {
                await _chatAppContext.UserConnections.AddAsync(userConnection);
                await _chatAppContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task<UserConnection> FetchConnectedUser(string searchTerm)
        {
            UserConnection? userConnection = null;
            if(int.TryParse(searchTerm, out int userId)) {
                userConnection = await _chatAppContext.UserConnections.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            }else
            {
                userConnection = await _chatAppContext.UserConnections.Where(x => x.ConnectionId == searchTerm).FirstOrDefaultAsync();
            }
            return userConnection;
        }

        public async Task RemoveUserConnection(UserConnection userConnection)
        {
            if(_chatAppContext.UserConnections.Contains(userConnection))
            {
                _chatAppContext.UserConnections.Remove(userConnection);
                await _chatAppContext.SaveChangesAsync();
            }
        }
    }
}
