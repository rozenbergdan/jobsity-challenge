using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : Controller
    {
        [HttpPost("message")]
        public string Index()
        {
            return "";
        }
    }
}
