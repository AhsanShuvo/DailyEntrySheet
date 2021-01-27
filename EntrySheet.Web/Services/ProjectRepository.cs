using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context): base(context)
        {
        }

        public bool AddProject(Project model)
        {
            try
            {
                _context.Projects.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                /// logger will be here 
                return false;
            }
        }

        public List<Project> GetProjects()
        {
            try
            {
                return _context.Projects.ToList();
            }
            catch(Exception e)
            {
                return new List<Project>();
            }
        }

        public Project GetProject(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);
                if(project != null)
                {
                    return project;
                }
                else
                {
                    /// logger 
                    return new Project();
                }
            }
            catch(Exception e)
            {
                /// logger
                return new Project();
            }
        }
    }
}
