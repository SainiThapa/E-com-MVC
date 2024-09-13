namespace EcomMVC.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string BuyerId { get; set; }

        public bool isActive {get; set;} = true;

        public DateTime CreatedDate{ get; set; }
        
        public List<CartItem> Items { get; set; }=new List<CartItem>();
    }
}
