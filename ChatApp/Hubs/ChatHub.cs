using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ChatApp.Hubs
{
    public class ChatHub: Hub<IChatHub>
    {
        private readonly ISignalService signalService;
        private readonly IChatService chatService;
        public ChatHub(ISignalService signalService, IChatService chatService) 
        {
            this.chatService = chatService;
            this.signalService = signalService;
        }

        public override async Task OnConnectedAsync()
        {

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userConnectionId = Context.ConnectionId;
            UserConnection onlineUser = await this.signalService.FetchConnectedUser(userConnectionId);

            if (onlineUser != null)
            {
                await this.signalService.RemoveUserConnection(onlineUser);
                Console.WriteLine("disconnected");
            }

            if (exception != null)
            {
                Console.WriteLine(exception.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUser(int userId)
        {
            UserConnection newUserConnection = new UserConnection()
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId,
            };
            await this.signalService.AddOnlineUser(newUserConnection);
        }

        public async Task LogOutUser(int userId)
        {
            UserConnection userConnectionToBeLoggedOut = await this.signalService.FetchConnectedUser(userId.ToString());
            if(userConnectionToBeLoggedOut != null)
            {
                await this.signalService.RemoveUserConnection(userConnectionToBeLoggedOut);
                Clients.Caller.StopConnection();
            }
        }

        //public async Task DeleteChat(int chatId)
        //{
            
        //}


 
        public async Task SendChat(ChatViewModel chat)
        {
            Chat chatFromDb = await this.chatService.SendChat(chat);

            UserConnection sender = await this.signalService.FetchConnectedUser(chatFromDb.SenderId.ToString());
            UserConnection reciever = await this.signalService.FetchConnectedUser(chatFromDb.RecieverId.ToString());
            string senderConnectionId = sender.ConnectionId;

            try
            {
                if (reciever != null)
                {
                    await Clients.Clients(senderConnectionId, reciever.ConnectionId).RecieveMessage(chatFromDb);
                }
                else
                {
                    await Clients.Client(senderConnectionId).RecieveMessage(chatFromDb);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public async Task ReplyToChat(ChatViewModel reply)
        //{
        //    Chat chatFromDb = await chatService.ReplyToChat(reply);

        //    UserConnection sender = await this.signalService.FetchConnectedUser(chatFromDb.SenderId.ToString());
        //    UserConnection reciever = await this.signalService.FetchConnectedUser(chatFromDb.RecieverId.ToString());
        //    string senderConnectionId = sender.ConnectionId;

        //}

    }
}
