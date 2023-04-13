using System.ComponentModel.DataAnnotations;

namespace BooksRating.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Електронна пошта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage ="Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
