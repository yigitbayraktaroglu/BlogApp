using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen kullanızı adınızı giriniz")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen Mailinizi giriniz")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler uyumlu değil, kontrol ediniz")]
        public string PasswordConfirm { get; set; }
    }
}
