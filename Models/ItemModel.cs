using System.ComponentModel.DataAnnotations;
namespace EcomMVC.Models{
    public class Item{
        public Product Product{ get; set; }
        public int Quantity { get; set; }
    }
}