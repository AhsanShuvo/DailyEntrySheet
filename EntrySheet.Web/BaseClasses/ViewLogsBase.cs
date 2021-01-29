using EntrySheet.Domain.Enums;
using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Component;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class ViewLogsBase : ComponentBase
    {
        public LogHistoryFilterModel LogHistoryFilterModel { get; set; }
        public List<IdentityUser> Users { get; set; }
        public List<Project> Projects { get; set; }
        [Inject]
        public IUserRepository UserRepository { get; set; }
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public LogHistoryGridview LogView { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LogHistoryFilterModel = new LogHistoryFilterModel();
            Users = new List<IdentityUser>();
            Projects = new List<Project>();
            await PopulateFieldData();
            StateHasChanged();
        }

        public void ShowLogsByFilter()
        {
            LogView.Refresh();
        }

        private async Task PopulateFieldData()
        {
            var claims = await UserService.GetUserClaims();
            var role = claims[3].Value.ToString();
            Enum.TryParse(role, out Role userRole);
            if(userRole == Role.User)
            {
                Users.Clear();
                Users.Add(UserRepository.GetUser(claims[0].Value.ToString()));
            }
            else
            {
                Users = UserRepository.GetUsers();
            }
            Projects = ProjectRepository.GetProjects();
            LogHistoryFilterModel.StartDate = DateTime.Now.AddDays(-7);
            LogHistoryFilterModel.EndDate = DateTime.Now;
            LogHistoryFilterModel.Project = (ProjectUserRepository.GetAssignedProject(claims[0].Value)).ProjectRef.Name;
            LogHistoryFilterModel.UserName = claims[1].Value;
        }
    }
}
