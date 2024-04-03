using System.ComponentModel.DataAnnotations;

namespace AppMVC.PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "User Name is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "First Name is Required")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is Required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5 ")]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password does not match with Password")]
		public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }


    }
}
