using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Data;
using EcomMVC.Models;
using Microsoft.EntityFrameworkCore;
using EcomMVC.ViewModel;
using EcomMVC.Data.Infrastructure;

namespace EcomMVC.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationRepository _authRepository;

        public AccountController(IAuthenticationRepository AuthRepository)
        {
            _authRepository = AuthRepository;
        }

        [HttpGet]   
        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl=null)
        {
            if(ModelState.IsValid)
            {
                var user =await _authRepository.AuthenticateUser(model.Email, model.Password);
                if (user!=null)
                {
                    if(!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index","Admin");
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index","Home");
                    }
                     else
                    {
                        // If user creation failed, add a generic error message to the ModelState
                        Console.WriteLine("User authentication failed");
                        ModelState.AddModelError("", "Invalid Login credential. Please try again.");
                    }
                }
            }
            else
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
            }
            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
            public async Task <IActionResult> SignUp(UserViewModel model)
            {
                if(ModelState.IsValid)
                {
                    User user =  new User{
                        Name=model.Name,
                        UserName =  model.Name,
                        Email = model.Email,
                        PhoneNumber=model.PhoneNumber
                    };
                    bool result =await _authRepository.CreateUser(user,model.Password);
                    // Console.WriteLine(model.Email+" "+model.PhoneNumber+" "+model.Password);
                    if(result)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        // If user creation failed, add a generic error message to the ModelState
                        ModelState.AddModelError("", "Failed to create the account. Please try again.");
                    }
                }
            return View(model);
            }

        public new IActionResult SignOut()
        {
            _authRepository.SignOut();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Unauthorize(){
            return View();
        }
}
}
