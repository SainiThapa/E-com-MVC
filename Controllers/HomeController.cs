using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Models;

namespace EcomMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            // Check if the user is in the Admin role
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            // Check if the user is in the User role
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Item");
            }
        }

        // If not authenticated, go to the default Home Index
        return View();
    }
  

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
