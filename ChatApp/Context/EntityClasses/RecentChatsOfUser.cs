namespace ChatApp.Context.EntityClasses
{
    public class RecentChatsOfUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string Username { get; set; }
        public string ChatContent { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public DateTime CreatedAt { get; set; }
        //public long RowNumber { get; set; }
    }
}
