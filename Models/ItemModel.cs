using System.ComponentModel.DataAnnotations;
namespace EcomMVC.Models{
   public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public string ImagePath { get; set; } // Image for the item
    public string SellerId { get; set; } // Foreign key for the seller (ApplicationUser)
    public ApplicationUser Seller { get; set; }
}

}