using EntrySheet.Web.Utilities;
using EntrySheet.Web.ViewModels;
using EntrySheet.Web.Component;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        private List<Claim> Claims { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Claims = await UserService.GetUserClaims();
            LogHistoryFilterModel = new LogHistoryFilterModel();
            Users = new List<IdentityUser>();
            Projects = new List<Project>();
            PopulateFieldData();
        }

        public void ShowLogsByFilter()
        {
            LogView.Refresh();
        }

        private void PopulateFieldData()
        {
            var role = Claims[3].Value.ToString();
            Enum.TryParse(role, out Role userRole);
            if(userRole == Role.User)
            {
                Users.Clear();
                Projects.Clear();
                Users.Add(UserRepository.GetUser(Claims[0].Value.ToString()));
                var assignedProject = ProjectUserRepository.GetAssignedProject(Claims[0].Value.ToString());
                if(assignedProject != null)Projects.Add(assignedProject.ProjectRef);
            }
            else
            {
                Users = UserRepository.GetUsers();
                Projects = ProjectRepository.GetProjects();
            }
            LogHistoryFilterModel.StartDate = DateTime.Now.AddDays(-7);
            LogHistoryFilterModel.EndDate = DateTime.Now;
            var project = ProjectUserRepository.GetAssignedProject(Claims[0].Value);
            if (project != null) LogHistoryFilterModel.Project = project.ProjectRef.Name ?? project.ProjectRef.Name;
            LogHistoryFilterModel.UserName = Claims[1].Value;
        }
    }
}
