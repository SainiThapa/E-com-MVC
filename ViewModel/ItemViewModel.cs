using System.ComponentModel.DataAnnotations;
using EcomMVC.Models;

namespace EcomMVC.ViewModel{
    public class ItemViewModel
    {    public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int ItemId { get; set; }

        [Display(Name = "Image Path")]
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}