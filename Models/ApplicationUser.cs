using Microsoft.AspNetCore.Identity;

namespace EcomMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required string PhotoPath { get; set; } // Store profile photo path
        public required string Role { get; set; } // "Merchant" or "Customer"
    }
}
