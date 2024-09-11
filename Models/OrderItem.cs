namespace EcomMVC.Models
{
    public class OrderItem
    {
        public OrderItem(int itemId, int quantity, int price, decimal total)
        {
            ItemId= itemId;
            Quantity= quantity;
            Price= price;
            Total= total;
        }   
        public int Id { get; set; }
        public int ItemId { get; set;}
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}