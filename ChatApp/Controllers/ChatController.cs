using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        public ChatController(IChatService _chatService) 
        {
            chatService = _chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendChat(ChatViewModel chat)
        {
            if (chat == null)
            {
                return BadRequest("chat is null");
            }
            var newChat = await chatService.SendChat(chat);
            return Ok(newChat);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteChat(int chatId, int userId)
        {
            if(await chatService.DeleteChat(chatId, userId))
            {
                return Ok(true);
            }
            return BadRequest(false);
            
        }



        [HttpPost("reply")]
        public async Task<IActionResult> ReplyToChat(ChatViewModel reply)
        {

            if(reply == null || reply.ChatContent == null)
            {
                return BadRequest("reply is null");
            }
            Chat newReply = await chatService.ReplyToChat(reply);
            return Ok(newReply);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatById(int id)
        {
            if(id != 0)
            {
                var chat = await chatService.GetChatById(id);
                return Ok(chat);
            }
            return BadRequest("chat not found");
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetAllChatsOfUser(int id)
        {
            if(id != 0)
            {
                var chatList = await chatService.GetAllChatsOfUser(id);
                return Ok(chatList);
            }
            return BadRequest("id not found");
        }

        [HttpGet("all-recents")]
        public IActionResult GetAllRecentChatsOfUser(int id)
        {
            
                var recentChats = chatService.GetAllRecentChatsOfUser(id);
                return Ok(recentChats);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetChatsBetweenTwoContacts(int SenderId, int RecieverId)
        {
            var chats = await chatService.GetAllChatsBetweenTwoContacts(SenderId, RecieverId);
            
            return Ok(chats);
        }

    }
}