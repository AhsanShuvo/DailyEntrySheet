using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ApplicationDbContext context, ILogger<ProjectRepository> logger): base(context)
        {
            _logger = logger;
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
                _logger.LogError(e, "Failed to add project to the database server.");
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
                _logger.LogError(e, "Failed to connect to the database to fetch projects.");
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
                    _logger.LogInformation("Successfully fetch project from database.");
                    return project;
                }
                else
                {
                    _logger.LogInformation("No project found");
                    return new Project();
                }
            }
            catch(Exception e)
            {
                _logger.LogInformation(e, "Failed to connect to the database to get project");
                return new Project();
            }
        }

        public bool UpdateProject(Project model)
        {
            try
            {
                _context.Attach(model);
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to update project.");
                return false;
            }
        }

        public void RemoveProject(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove project.");
            }
        }

        public bool SearchProjectName(string name)
        {
            try
            {
                return  _context.Projects.Any(m => m.Name == name);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to search project.");
                return false;
            }
        }
    }
}
