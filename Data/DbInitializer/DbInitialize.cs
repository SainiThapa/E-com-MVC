using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcomMVC.Data.DbInitializer
{
    public class DbInitialize: IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception){
                throw;
            }

            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new User
            {
                UserName="admin@gmail.com",
                Email = "admin@gmail.com",
                Name="Admin",
                PhoneNumber="012223922"
            }, "Admin@123").GetAwaiter().GetResult();

            User user = _context.Users.FirstOrDefault(x=>x.Email=="admin@gmail.com");
            _userManager.AddToRoleAsync(user,"Admin").GetAwaiter().GetResult();
            }
            return;
        }
    }   
}