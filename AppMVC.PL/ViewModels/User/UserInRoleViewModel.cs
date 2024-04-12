using System;

namespace AppMVC.PL.ViewModels.User
{
    public class UserInRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }

        internal void Add()
        {
            throw new NotImplementedException();
        }
    }
}
