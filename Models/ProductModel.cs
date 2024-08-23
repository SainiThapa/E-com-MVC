using System.ComponentModel.DataAnnotations;

namespace EcomMVC.Models
{
    public class Product
    {
        public string? Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string? Photo { get; set; }
    }
}