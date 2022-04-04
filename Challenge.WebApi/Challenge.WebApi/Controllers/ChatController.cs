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
        //private Func<string, IMessage> messageService;
        //public ChatroomController(Func<string,IMessage> messageService)
        //{
        //    this.messageService = messageService;   
        //}

        [HttpPost("{chatroom}/message/send")]
        [Authorize]
        public IActionResult SendMessage(int chatroom, [FromBody] MessageDTO message)
        {
            if (string.IsNullOrEmpty(message.Message))
                return BadRequest("Message empty");
            
            //this.messageService(message.Message).Send(new Message
            //{
            //    Chatroom = chatroom,
            //    Content = message.Message,
            //    Date = DateTimeOffset.UtcNow,
            //    Username = "goku" //HttpContext.User.Identity.Name,
            //});
            return Ok();
        }
    }
}
