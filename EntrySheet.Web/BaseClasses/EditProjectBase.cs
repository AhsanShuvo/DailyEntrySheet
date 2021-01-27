using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EntrySheet.Web.BaseClasses
{
    public class EditProjectBase : ComponentBase
    {
        public Project ProjectInfo { get; set; }
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ProjectInfo = new Project();
            if(Id != 0)
            {
                ProjectInfo = ProjectRepository.GetProject(Id);
            }
        }

        public void SubmitProject()
        {
            var response = ProjectRepository.AddProject(ProjectInfo);
            NavigationManager.NavigateTo("projects");
        }
    }
}
