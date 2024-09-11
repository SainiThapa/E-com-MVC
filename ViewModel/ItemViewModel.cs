using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel{
    public class ItemViewModel
    {    public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ItemId { get; set; }

        public double? Price { get; set; }
        public int? QuantityAvailable { get; set; }
        public decimal Total { get; set; }
        public string? ImagePath { get; set; }
    }
}