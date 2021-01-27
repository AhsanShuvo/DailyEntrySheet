using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace EntrySheet.Web.BaseClasses
{
    public class UsersBase : ComponentBase
    {
        public List<UserViewModel> Users { get; set; }
        public string Title { get; set; }
        [Inject]
        public IUserRepository UserRepository { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Title = "User List";
            Users = new List<UserViewModel>();
        }
    }
}
