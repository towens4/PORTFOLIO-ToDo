using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "An email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }

        
    }
}
