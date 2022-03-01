using EntrySheet.Web.Utilities;
using EntrySheet.Web.ViewModels;
using EntrySheet.Web.Component;
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
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }
        public CustomAutoGrid<UserViewModel> GridView { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Title = "User List";
            Users = new List<UserViewModel>();
            PopulateUsers();
        }

        private void PopulateUsers()
        {
            Users.Clear();
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

        public void EditUser(string id)
        {
            NavigationManager.NavigateTo("edituser/" + id);
        }

        public void DeleteUser(string id)
        {
            UserRepository.DeleteUser(id);
            var projectUser = ProjectUserRepository.GetAssignedProject(id);
            ProjectUserRepository.RemoveAssignedUser(projectUser);
            UserLogRepository.RemoveUserLogsByUserId(id);
            UpdateChild();
            
        }

        private void UpdateChild()
        {
            PopulateUsers();
            GridView.Refresh();
        }
    }
}
