using Microsoft.AspNetCore.Mvc;

namespace EcomMVC.Areas.User.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}