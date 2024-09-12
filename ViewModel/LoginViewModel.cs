using System.ComponentModel.DataAnnotations;

namespace EcomMVC.ViewModel{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}