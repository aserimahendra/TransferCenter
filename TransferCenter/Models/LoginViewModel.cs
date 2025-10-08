using System.ComponentModel.DataAnnotations;

namespace TransferCenter.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email/Login ID is required")]
        [Display(Name = "Email/Login ID")]
        public string LoginId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}