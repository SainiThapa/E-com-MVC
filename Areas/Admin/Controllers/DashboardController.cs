using Microsoft.AspNetCore.Mvc;

namespace EcomMVC.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}