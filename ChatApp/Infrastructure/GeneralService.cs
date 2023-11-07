using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure
{
    public class GeneralService : IGeneralService
    {
        private readonly ChatAppContext context;
        public GeneralService(ChatAppContext _context) 
        {
            context = _context;
        }

        public async Task<List<SearchResult>> SearchUser(string searchQuery, int currentUserId)
        {
            List<SearchResult> searchResults = null;
            if(searchQuery != null)
            {
                searchResults = await context.SearchResults.FromSqlRaw("Exec dbo.spSearchUsers @p0, @p1", searchQuery, currentUserId).ToListAsync();
            }

            return searchResults;
        }

        public async Task<SearchedUser> GetUserById(int id)
        {
            SearchedUser user = null;
            if(id > 0)
            {
                user = await context.SearchedUsers.FromSqlRaw("Exec dbo.spGetUserById @p0", id).FirstOrDefaultAsync();
            }
            return user;
        }
    }
}
