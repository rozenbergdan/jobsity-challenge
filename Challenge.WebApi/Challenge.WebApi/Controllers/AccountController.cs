using Challenge.Domain.DTO;
using Challenge.Domain.Entities;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public Task<string> Login([FromBody]LoginDTO login) 
        {
            return accountService.Login(new Login { UserName = login.Username, Password = login.Password });
        }


        [HttpGet("login")]
        [Authorize]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
