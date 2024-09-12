namespace EcomMVC.Models
{
    public class OrderItem
    {
        public OrderItem()
        {}
        public int Id { get; set; }
        public int ItemId { get; set;}
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public OrderItem(int itemId, int quantity, decimal price, decimal total)
        {
            ItemId= itemId;
            Quantity= quantity;
            Price= price;
            Total= total;
        }   
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}