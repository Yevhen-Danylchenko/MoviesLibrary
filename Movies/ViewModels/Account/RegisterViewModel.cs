using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть email")]
        [EmailAddress(ErrorMessage = "Невірний формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть ім'я")]
        [Display(Name = "Ім'я")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Від 2 до 50 символів")]
        public string DisplayName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Мінімум 4 символи")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Підствердіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердженян паролю")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}