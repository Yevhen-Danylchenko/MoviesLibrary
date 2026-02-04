using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels.Admin
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        [Display(Name = "Login")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Login must be between 2 and 50 characters")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
