using System.Collections.Generic;
namespace EcomMVC.Models
{
    public class Order
    {
        public string Id{ get; set; }
        public int UserId { get; set; }
        public string PaymentId { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public DateTime CreatedDate { get; set;}

        public ICollection<OrderItem> OrderItems { get; set; }  = new HashSet<OrderItem>();

        public string Locality { get; set; }

        public string PhoneNumber { get; set; }
    }
}