using Challenge.Domain.DTO;
using Challenge.Domain.Entities;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    [Route("api/hi")]
    [ApiController]
    public class HiController : Controller
    {
        private IAccountService accountService;
        public HiController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("login")]
        [Authorize]
        public string Login()
        {
            return "asdasd";
        }

    }
}
