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
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        public DbInitialize(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task Initialize()
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

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                Console.WriteLine("RoleExists not");
                await _roleManager.CreateAsync(new Role {Name="Admin", Description = "SuperUser Role"});
                await _roleManager.CreateAsync(new Role {Name="Merchant", Description = "Seller User"});
                await _roleManager.CreateAsync(new Role {Name="User", Description = "Customer User"});

            await _userManager.CreateAsync(new User
            {
                UserName="admin@gmail.com",
                Email = "admin@gmail.com",
                Name="Admin",
                PhoneNumber="012223922"
            }, "Admin@123");

            User? user = await _context.Users.FirstOrDefaultAsync(x=>x.Email=="admin@gmail.com");
            await _userManager.AddToRoleAsync(user,"Admin");
            }
            return;

            // Create the Admin user if it doesn't already exist
            // if (await _userManager.FindByEmailAsync("admin@gmail.com") == null)
            // {
            //     User adminUser = new User
            //     {
            //         UserName = "admin@gmail.com",
            //         Email = "admin@gmail.com",
            //         Name = "Admin",
            //         PhoneNumber = "012223922"
            //     };

            //     // Create the admin user
            //     var result = await _userManager.CreateAsync(adminUser, "Admin@123");
            //     if (result.Succeeded)
            //     {
            //         // Add the admin user to the Admin role
            //         await _userManager.AddToRoleAsync(adminUser, "Admin");
            //     }
            // }
        }
    }   
}