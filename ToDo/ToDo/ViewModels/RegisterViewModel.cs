using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "An email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required")]
        [Compare("Password", ErrorMessage = "Passwords must be the same")]
        public string ConfirmPassword { get; set; }
    }
}
