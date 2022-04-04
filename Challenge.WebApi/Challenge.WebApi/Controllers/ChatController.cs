using Challenge.Domain.DTO;
using Challenge.Domain.Entities;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    [Route("/api/chatroom")]
    [ApiController]
    public class ChatroomController : BaseController
    {
        private Func<string, IMessage> messageService;
        private readonly IConfiguration config;

        private IChatRoomService chatRoomService { get; }

        public ChatroomController(Func<string, IMessage> messageService, IChatRoomService chatRoomService, IConfiguration config)
        {
            this.messageService = messageService;
            this.chatRoomService = chatRoomService;
            this.config = config;
        }

        [HttpPost("{chatroom}/messages/send")]
        [Authorize]
        public IActionResult SendMessage(int chatroom, [FromBody] MessageDTO message)
        {
            if (string.IsNullOrEmpty(message.Message))
                return BadRequest("Message empty");

            this.messageService(message.Message).Send(new Message
            {
                Chatroom = chatroom,
                Content = message.Message,
                Date = DateTime.UtcNow,
                Username = HttpContext.User.Identity.Name,
            });
            return Ok();
        }

        [HttpGet("{chatroom}/messages")]
        [Authorize]
        public IEnumerable<ChatRoomMessageDTO> GetAll(int chatroom)
        {
            return chatRoomService.GetLasts(chatroom, config.GetValue<int>("chatroomSize"));
        }
    }
}
