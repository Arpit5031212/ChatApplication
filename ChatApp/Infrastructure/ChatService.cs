using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.Infrastructure
{
    public class ChatService : IChatService
    {
        private readonly ChatAppContext context;

        public ChatService(ChatAppContext _context) 
        {
            context = _context;
        }
        public async Task<Boolean> DeleteChat(int chatId, int userId)
        {
            if (chatId != 0 && userId != 0)
            {
                var chat = await context.Chats.FindAsync(chatId);
                if (chat != null)
                {
                    if(userId == chat.SenderId)
                    {
                        chat.IsDeletedBySender = true;
                    }
                    else
                    {
                        chat.IsDeletedByReciever = false;
                    }
                    context.Chats.Update(chat);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<Chat> EditChat(int id, ChatViewModel chat)
        {
            Chat chatToBeEdited = await context.Chats.FindAsync(id);
            if (chatToBeEdited != null)
            {
                chatToBeEdited.ChatContent = chat.ChatContent;
                chatToBeEdited.UpdatedAt = DateTime.UtcNow;
                chatToBeEdited.ChatType = chat.ChatType;

                context.Chats.Update(chatToBeEdited);
                await context.SaveChangesAsync();
            }
            return chatToBeEdited;
        }

        public async Task<List<Chat>> GetAllChatsBetweenTwoContacts(int SenderId, int RecieverId)
        {

           //var chatList = await context.Chats.Where(c => (c.SenderId == SenderId && c.RecieverId == RecieverId) || (c.SenderId == RecieverId && c.RecieverId == SenderId)).Include(c => c.SenderProfile).Include(c => c.RecieverProfile).ToListAsync();
            var chatList = await context.Chats.Where(c => (c.SenderId == SenderId && c.RecieverId == RecieverId) || (c.SenderId == RecieverId && c.RecieverId == SenderId)).ToListAsync();

            return chatList;
        }

        public List<RecentChatsOfUser> GetAllRecentChatsOfUser(int id)
        {
            List<RecentChatsOfUser> recentChatList = context.AllRecentChats.FromSqlRaw("Exec dbo.spGetAllRecentChatsOfUser @p0", id).ToList();
           
            return recentChatList;
            
        }

        public async Task<List<Chat>> GetAllChatsOfUser(int id)
        {
            var chatList = await context.Chats.Where(c => c.RecieverId == id || c.SenderId == id).Include(c => c.SenderProfile).Include(c => c.RecieverProfile).OrderBy(c => c.CreatedAt).ToListAsync();
            return chatList;
        }

        public async Task<Chat> GetChatById(int id)
        {
            if (id != 0)
            {
                return await context.Chats.Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<Chat> SendChat(ChatViewModel chat)
        {
            Chat newChat = null;

            if (chat != null) 
            {
                newChat = new Chat()
                {
                    ChatType = chat.ChatType,
                    ChatContent = chat.ChatContent,
                    RepliedTo = chat.RepliedTo,
                    SenderId = chat.SenderId,
                    RecieverId = chat.RecieverId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await context.Chats.AddAsync(newChat);
                await context.SaveChangesAsync();
            }
            return newChat;

        }

        public async Task<Chat> ReplyToChat(ChatViewModel chatReply)
        {
            Chat newChatReply = null;
            if (chatReply.ChatContent != null)
            {
                newChatReply = new Chat()
                {
                    ChatType = chatReply.ChatType,
                    ChatContent = chatReply.ChatContent,
                    RepliedTo = chatReply.RepliedTo,
                    SenderId = chatReply.SenderId,
                    RecieverId = chatReply.RecieverId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await context.Chats.AddAsync(newChatReply); 
                await context.SaveChangesAsync();
            }
            return newChatReply;
        }
    }
}
