using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EcomMVC.Areas.User.Controllers
{
    [Authorize(Roles = "User")] // Restrict access to Admins only
    [Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}