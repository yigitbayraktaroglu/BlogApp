using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Lütfen mail giriniz")]

		public string UserName { get; set; }

		[Required(ErrorMessage = "Lütfen şifreyi giriniz")]
		public string Password { get; set; }
	}
}
