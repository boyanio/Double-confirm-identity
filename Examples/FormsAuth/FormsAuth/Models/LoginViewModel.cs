using System.ComponentModel.DataAnnotations;

namespace DoubleConfirmIdentity.Examples.FormsAuth.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Confirm me?")]
        public bool ConfirmMe { get; set; }
    }
}
