using System.Collections.Generic;
namespace EcomMVC.Models
{
    public class Order
    {
        public string Id{ get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string PaymentId { get; set; }

        public DateTime CreatedDate { get; set;}

        public ICollection<OrderItem> OrderItems { get; set; }  = new HashSet<OrderItem>();
    }
}