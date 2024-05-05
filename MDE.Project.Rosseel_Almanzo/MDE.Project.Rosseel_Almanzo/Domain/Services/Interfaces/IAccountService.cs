using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> Login(string username, string password);
        Task<bool> Register(User newUser);
        Task<bool> DeleteAccountAsync(string id);
    }
}
