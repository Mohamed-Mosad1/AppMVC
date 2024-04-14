using System.ComponentModel.DataAnnotations;

namespace AppMVC.PL.ViewModels.User
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
