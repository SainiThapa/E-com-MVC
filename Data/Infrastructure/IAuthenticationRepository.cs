using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomMVC.Models;

namespace EcomMVC.Data.Infrastructure
{
    public interface IAuthenticationRepository
    {
        bool CreateUser(User user, string password);
        Task<bool> SignOut();
        User AuthenticateUser(string Username, string Password);
        User GetUser(string Username);  
    }
}