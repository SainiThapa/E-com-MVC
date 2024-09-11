using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter Password")]
        public string Password { get; set; }

    }
}