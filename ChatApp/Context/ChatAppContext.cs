using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Context
{
    public class ChatAppContext: DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options): base(options) { }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<RecentChatsOfUser> AllRecentChats { get; set; }
        public virtual DbSet<SearchResult> SearchResults { get; set; }
        public virtual DbSet<SearchedUser> SearchedUsers { get; set; }
        public virtual DbSet<UserConnection> UserConnections { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecentChatsOfUser>().HasNoKey().ToView("spGetAllRecentChatsOfUser");
            modelBuilder.Entity<SearchResult>().HasNoKey().ToView("spSearchUsers");
            modelBuilder.Entity<SearchedUser>().HasNoKey().ToView("spGetUserById");
            modelBuilder.Entity<UserConnection>().HasKey(key => new { key.UserId, key.ConnectionId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
