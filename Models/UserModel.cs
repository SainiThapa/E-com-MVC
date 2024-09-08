using EcomMVC.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string PhotoPath { get; set; } // For profile photo
}
