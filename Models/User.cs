using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EcomMVC.Models{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        

        [NotMapped]
        public string[]? Roles{ get; set; }
    }
        
}