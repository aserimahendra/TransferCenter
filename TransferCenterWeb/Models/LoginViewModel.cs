using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login ID is required")]
        [Display(Name = "Login ID")]
        public string LoginId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}