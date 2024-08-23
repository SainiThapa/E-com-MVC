namespace EcomMVC.Models
{
    public class Cart
    {
        public string? Id {get; set;}
        public List<Item> Items {get; set;}
        
        public Cart(){
            Items=new List<Item>();
        }

    }
}