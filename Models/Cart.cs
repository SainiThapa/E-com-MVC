namespace EcomMVC.Models
{
    public class Cart
    {
        public int? Id { get; set; }
        public string? BuyerId { get; set; }
        // public ApplicationUser? Buyer { get; set; }

        public bool isActive {get; set;} = true;
        // public int? ItemId { get; set; }
        // public Item? Item { get; set; }
        
        public List<CartItem> Items { get; set; }=new List<CartItem>();
        // public int? Quantity { get; set; }
    }
}
