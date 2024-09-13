using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcomMVC.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity{ get; set; }
        public string? ImagePath { get; set; }
        public string? SellerId { get; set; }
        
        public ApplicationUser? Seller { get; set; }
    }
}
