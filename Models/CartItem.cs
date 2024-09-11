using System.Text.Json.Serialization;

namespace EcomMVC.Models
{
    public class CartItem
    {
        public CartItem(int itemId, int quantity, decimal price, string name)
        {
            ItemId = itemId;
            Quantity = quantity;
            Price = price;
        }
        public int Id { get; set;}
        public Guid CartId  { get; set;}
        
        public int ItemId { get; set;}
        public int Quantity { get; set;}

        public decimal Price { get; set;}
        [JsonIgnore]
        public Cart Cart{ get; set;}
    }
}