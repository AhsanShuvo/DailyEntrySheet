using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace EntrySheet.Web.BaseClasses
{
    public class ProjectsBase : ComponentBase
    {
        public List<Project> Projects { get; set; }
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }
        public string Title { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Projects = new List<Project>();
            Projects = ProjectRepository.GetProjects();
            Title = "Project List";
        }
    }
}
