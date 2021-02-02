using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.BaseClasses
{
    public class AssignUserBase : ComponentBase
    {
        public bool IsDisabled { get; set; }
        public List<Project> Projects { get; set; }
        public List<IdentityUser> Users { get; set; }
        public ProjectUser ProjectUser { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public List<ProjectUser> AssignedUsers { get; set; }
        [Inject]
        public IUserRepository UserRepository { get; set; }
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized(); 
            Projects = new List<Project>();
            Users = new List<IdentityUser>();
            ProjectUser = new ProjectUser();
            InitializeData();
        }

        private void InitializeData()
        {
            InitUsers();
            InitPorjects();
            UserId = null;
            if(ProjectId != 0)FillAssignedUser();
            if(Users.Count > 0) UserId = Users[0].Id;
            TriggerDisabled();
        }

        private void InitUsers()
        {
            Users = UserRepository.GetFilteredUsers();
        }

        private void InitPorjects()
        {
            Projects = ProjectRepository.GetProjects();
        }

        public void AssignNewUser()
        {
            if(ProjectId != 0 && UserId != null)
            {
                ProjectUser = new ProjectUser
                {
                    ProjectRef = Projects.Where(x => x.Id == ProjectId).FirstOrDefault(),
                    UserRef = Users.Where(x => x.Id == UserId).FirstOrDefault()
                };

                var res = ProjectUserRepository.AddNewUser(ProjectUser);
                InitializeData();
            }
        }

        public void ShowAssignedUser(ChangeEventArgs args)
        {
            ProjectId = Convert.ToInt32(args.Value);
            FillAssignedUser();
            TriggerDisabled();
        }

        private void FillAssignedUser()
        {
            AssignedUsers = ProjectUserRepository.GetAssignedUsers(ProjectId);
        }

        public void RemoveAssignedUser(int id)
        {
            var projectuser = AssignedUsers.Where(m => m.Id == id).FirstOrDefault();
            ProjectUserRepository.RemoveAssignedUser(projectuser);
            InitializeData();
        }

        public void Refresh(int projectId)
        {
            var projectToRemove = Projects.SingleOrDefault(x => x.Id == projectId);
            Projects.Remove(projectToRemove);
            InitializeData();
            StateHasChanged();
        }

        private void TriggerDisabled()
        {
            if (ProjectId > 0 && UserId != null) IsDisabled = false;
            else IsDisabled = true;
        }
    }
}
