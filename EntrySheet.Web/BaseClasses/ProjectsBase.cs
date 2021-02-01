using EntrySheet.Web.Component;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.BaseClasses
{
    public class ProjectsBase : ComponentBase
    {
        public List<Project> Projects { get; set; }
        public string Title { get; set; }
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }
        public CustomAutoGrid<Project> GridView { get; set; }
        public AssignUser AssignUser { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Projects = new List<Project>();
            PopulateProjects();
            Title = "Project List";
        }

        public void EditProject(string id)
        {
            var projectId = Convert.ToInt32(id);
            NavigationManager.NavigateTo("editproject/" + projectId);
        }

        public void DeleteProject(string id)
        {
            var projectId = Convert.ToInt32(id);
            ProjectUserRepository.RemoveProject(projectId);
            UserLogRepository.RemoveUserLogsByProjectId(projectId);
            ProjectRepository.RemoveProject(projectId);
            UpdateChilds(projectId);

        }

        private void PopulateProjects()
        {
            Projects = ProjectRepository.GetProjects();
        }

        private void UpdateChilds(int projectId)
        {
            var projectToRemove = Projects.SingleOrDefault(x => x.Id == projectId);
            Projects.Remove(projectToRemove);
            GridView.Refresh();
            AssignUser.Refresh(projectId);
        }
    }
}
