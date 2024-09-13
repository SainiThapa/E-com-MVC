using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel{
    public class OrderViewModel
    {
        public string Id{ get; set; }
        public string PaymentId { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }

        public DateTime CreatedDate { get; set;}

        public ICollection<ItemViewModel> Items { get; set; }  = new List<ItemViewModel>();

        public string Locality { get; set; }
    }
}