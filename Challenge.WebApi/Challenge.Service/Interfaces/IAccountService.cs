using Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Interfaces
{
    public interface IAccountService
    {
        Task<string> Login(Login user);
    }
}
