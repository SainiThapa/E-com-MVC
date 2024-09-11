using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Data;
using EcomMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomMVC.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BrowseProducts()
        {
            var products = await _context.Products.ToListAsync();
            return View(products); // Customer page to browse products
        }

        public IActionResult ShoppingCart()
        {
            // Display shopping cart items for the customer
            return View();
        }

        public IActionResult Purchase()
        {
            // Handle purchasing logic
            return View();
        }

        // Order history, view product details, etc.
    }
}
