using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel
{
    public class UserViewModel 
    
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email{ get; set;}
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set;}

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set;}
        
        [Compare("Password",ErrorMessage ="Password donot match")]
        public string ConfirmPassword { get; set;}

        [Required(ErrorMessage ="Enter Phone number")]
        public string PhoneNumber { get; set;}

    }
}