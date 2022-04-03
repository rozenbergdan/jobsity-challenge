using Challenge.Domain.Entities;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Exceptions;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private IConfiguration _config;
        private IAuthentication _authentication;
        private UserManager<ChallengeUser> userManager;
        public AccountService(IConfiguration configuration, IAuthentication authentication, UserManager<ChallengeUser> userManager)
        {
            this._config = configuration;
            this._authentication = authentication;
            this.userManager = userManager;

        }
        public async Task<string> Login(Login user)
        {
            var useridentity = await userManager.FindByNameAsync(user.UserName);
            if (useridentity == null)
                throw new DomainException("The user doesn't exists.");

            if (await userManager.CheckPasswordAsync(useridentity, user.Password)) 
            {
                return this._authentication.Generate(user.UserName);
            }

            throw new DomainException("The password is incorrect");
        }
    }
}
