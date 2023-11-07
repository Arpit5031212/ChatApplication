using ChatApp.Context.EntityClasses;
using ChatApp.Models;

namespace ChatApp.Hubs
{
    public interface IChatHub
    {
        public Task RecieveMessage(Chat chat);
        public Task StopConnection();
    }
}
