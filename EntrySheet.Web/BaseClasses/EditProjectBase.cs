using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EntrySheet.Web.BaseClasses
{
    public class EditProjectBase : ComponentBase
    {
        public Project ProjectInfo { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
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
                Title = "Edit your project";
                ButtonText = "Update";
                ProjectInfo = ProjectRepository.GetProject(Id);
            }
            else
            {
                Title = "Create a new project";
                ButtonText = "Create";
            }
        }

        public void SubmitProject()
        {
            var response = false;
            if(Id > 0)
            {
                response = ProjectRepository.UpdateProject(ProjectInfo);
            }
            else
            {
                response = ProjectRepository.AddProject(ProjectInfo);
            }
            if(response == true)NavigationManager.NavigateTo("projects");
        }
    }
}
