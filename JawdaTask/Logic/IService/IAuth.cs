using JawdaTask.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.IService
{
    public interface IAuth
    {
        Task<bool> Login(LoginDTO model);
        Task<bool> Register(RegisterDTO model);
        Task<bool> Logout();
    }
}
