using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EcomMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}