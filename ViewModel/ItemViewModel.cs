using System.ComponentModel.DataAnnotations;
using EcomMVC.Models;

namespace EcomMVC.ViewModel{
    public class ItemViewModel
    {    public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ItemId { get; set; }
        public string ImagePath { get; set; }

        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal Total { get; set; }
    }
}