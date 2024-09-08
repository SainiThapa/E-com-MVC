namespace EcomMVC.Models
{
   public class CartItem
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public ApplicationUser Buyer { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}

}