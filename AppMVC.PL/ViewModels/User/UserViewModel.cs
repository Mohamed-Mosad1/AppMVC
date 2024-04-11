using System.Collections;
using System.Collections.Generic;

namespace AppMVC.PL.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
