using System.ComponentModel.DataAnnotations.Schema;

namespace EcomMVC.Models
{
    public class Item
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? QuantityAvailable { get; set; }
        public string? ImagePath { get; set; }
        public string? SellerId { get; set; }
        
        public ApplicationUser? Seller { get; set; }
    }
}
