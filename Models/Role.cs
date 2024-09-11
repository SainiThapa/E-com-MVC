using System;
using Microsoft.AspNetCore.Identity;

namespace EcomMVC.Models{
    public class Role:IdentityRole<int>
    {
        public string Description { get; set; }
    }
}