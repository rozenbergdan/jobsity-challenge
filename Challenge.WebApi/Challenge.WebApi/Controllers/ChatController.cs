using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    [Route("/api/chat")]
    [ApiController]
    public class ChatController : Controller
    {
        [HttpGet("message")]
        [Authorize(Roles = "User")]
        public string Login()
        {
            return "Llegamo";
        }
    }
}
