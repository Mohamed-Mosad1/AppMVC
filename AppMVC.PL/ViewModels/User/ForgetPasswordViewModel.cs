using System.ComponentModel.DataAnnotations;

namespace AppMVC.PL.ViewModels.User
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
