using EcomMVC.Data.Infrastructure;
using EcomMVC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Repositories
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public AuthenticationRepository(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> rolemanager)
        {
            _signInManager= signInManager;
            _userManager= userManager;
            _roleManager= rolemanager;
        }

        public User AuthenticateUser(string Username, string Password)
        {
            var result = _signInManager.PasswordSignInAsync(Username, Password,false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user=_userManager.FindByNameAsync(Username).Result;
                var roles= _userManager.GetRolesAsync(user).Result;
                user.Roles=roles.ToArray();

                return user;
            }
            return null;
        }

        public bool CreateUser(User user, string Password)
        {
            // Admin, User

            var result = _userManager.CreateAsync(user,Password).Result;
            if (result.Succeeded){
                string role ="User";
                var res= _userManager.AddToRoleAsync(user,role).Result;
                if(res.Succeeded)
                {
                    return true;
                }
            }
        }

        public User GetUser(string Username)
        {
            return _userManager.FindByNameAsync(Username).Result;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}