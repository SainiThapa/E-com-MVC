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

        public async Task<User> AuthenticateUser(string Username, string Password)
        {
            var result =await _signInManager.PasswordSignInAsync(Username, Password,false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user=await _userManager.FindByNameAsync(Username);
                var roles= await _userManager.GetRolesAsync(user);
                user.Roles=roles.ToArray();

                return user;
            }
            return null;
        }

        public async Task<bool> CreateUser(User user, string Password)
        {
            // Admin, User

            var result =await _userManager.CreateAsync(user,Password);
            if (result.Succeeded)
            {
                string role ="User";
                var res=await _userManager.AddToRoleAsync(user,role);
                return res.Succeeded;   
            }
            // If user creation fails, return false
            return false;
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