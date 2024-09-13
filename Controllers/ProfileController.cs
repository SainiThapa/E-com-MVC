using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Models;
using EcomMVC.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EcomMVC.Controllers;

public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;


    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet]
public async Task<IActionResult> ViewProfile()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return NotFound();
    }

    var model = new UserViewModel
    {
        Name = user.Name,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber
    };

    return View(model);
}

[HttpGet]
public async Task<IActionResult> EditProfile()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return NotFound();
    }

    var model = new UserViewModel
    {
        Name = user.Name,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber
    };

    return View(model);
}

[HttpPost]
public async Task<IActionResult> EditProfile(ProfileViewModel model)
{
    if (ModelState.IsValid)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        
        user.Name = model.Name;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("ViewProfile");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    return View(model);
}

}