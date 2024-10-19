using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EcomMVC.Models;
using EcomMVC.Data;
using EcomMVC.Data.Infrastructure;


namespace EcomMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class AdminController : Controller
    {
         private readonly IOrderRepository _orderRepository;
        private readonly UserManager<User> _userManager;
        public AdminController(IOrderRepository orderRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        // Action to display users who have made purchases
        public IActionResult UserList()
        {
            var orders = _orderRepository.GetAllOrders(); 

            var userIds = orders.Select(o => o.UserId).Distinct().ToList();

            var users = _userManager.Users.Where(u => userIds.Contains(u.Id.ToString())).ToList();

            return View(users);
        }
        public IActionResult Index()
        {
            return View();  
        }
    }
}