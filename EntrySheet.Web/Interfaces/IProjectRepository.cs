using EntrySheet.Web.Data;
using System.Collections.Generic;

namespace EntrySheet.Web.Interfaces
{
    public interface IProjectRepository
    {
        bool AddProject(Project model);
        List<Project> GetProjects();
        Project GetProject(int id);
    }
}
