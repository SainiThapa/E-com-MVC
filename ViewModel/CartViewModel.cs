using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public decimal Total { get; set; }

        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public IList<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}