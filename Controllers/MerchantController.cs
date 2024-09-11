using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Data;
using EcomMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EcomMVC.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class MerchantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = await _context.Products.Where(p => p.MerchantId == user.Id).ToListAsync();
            return View(products); // Merchant dashboard to manage their products
        }

        // Add/Edit/Delete Product Actions
    }
}
