using EntrySheet.Domain.Enums;
using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace EntrySheet.Web.BaseClasses
{
    public class UsersBase : ComponentBase
    {
        public List<UserViewModel> Users { get; set; }
        public string Title { get; set; }
        [Inject]
        public IUserRepository UserRepository { get; set; }
        [Inject]
        public IEntityModelBuilder EntityModelBuilder { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Title = "User List";
            Users = new List<UserViewModel>();
            PopulateUsers();
        }

        private void PopulateUsers()
        {
            var users = UserRepository.GetUsers();
            foreach(var user in users)
            {
                var userViewModel = EntityModelBuilder.GetUserViewModel(user);
                var role = UserRepository.GetUserRole(user.Id);
                Enum.TryParse(role, out Role userRole); ;
                userViewModel.Role = userRole;
                Users.Add(userViewModel);
            }
        }
    }
}
