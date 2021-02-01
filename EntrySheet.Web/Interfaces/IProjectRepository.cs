using EntrySheet.Web.Data;
using System.Collections.Generic;

namespace EntrySheet.Web.Interfaces
{
    public interface IProjectRepository
    {
        bool AddProject(Project model);
        List<Project> GetProjects();
        Project GetProject(int id);
        bool UpdateProject(Project model);
        void RemoveProject(int id);
        bool SearchProjectName(string name);
    }
}
