using System.Text.Json.Serialization;

namespace EcomMVC.Models
{
    public class CartItem
    {
        public int ItemId {get; set;}
        public int Quantity {get; set;}
        public decimal Price {get; set;}
        public string Name {get; set;}

   // Parameterless constructor for EF
        public CartItem() { }

    // Factory method
        public CartItem (int itemId, int quantity, decimal price, string name)
        {
                ItemId = itemId;
                Quantity = quantity;                
                Price = price;
                Name = name;
        }
        public int Id { get; set;}
        public Guid CartId  { get; set;}
        [JsonIgnore]
        public Cart Cart{ get; set;}
    }
}