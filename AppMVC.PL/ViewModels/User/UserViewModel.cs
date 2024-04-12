using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppMVC.PL.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
