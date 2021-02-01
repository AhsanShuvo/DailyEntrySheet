using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class ProjectUserRepository : BaseRepository, IProjectUserRepository
    {
        private readonly ILogger<ProjectUserRepository> _logger;
        public ProjectUserRepository(ApplicationDbContext context, ILogger<ProjectUserRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public bool AddNewUser(ProjectUser model)
        {
            try
            {
                _context.ProjectUsers.Add(model);
                _context.SaveChanges();
                _logger.LogInformation("Successfully added a new user.");
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server");
                return false;
            }
        }

        public List<ProjectUser> GetAssignedUsers(int projectId)
        {
            try
            {
                return _context.ProjectUsers
                            .Include(m => m.ProjectRef)
                            .Include(m => m.UserRef)
                            .Where(x => x.ProjectRef.Id == projectId)
                            .ToList();

            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the datbase server to fetch assigned users");
                return new List<ProjectUser>();
            }
        }

        public bool RemoveAssignedUser(ProjectUser model)
        {
            try
            {
                _context.ProjectUsers.Remove(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove user.");
                return false;
            }
        }

        public ProjectUser GetAssignedProject(string userId)
        {
            try
            {
                return _context.ProjectUsers
                    .Include(m => m.UserRef)
                    .Include(m => m.ProjectRef)
                    .Where(m => m.UserRef.Id == userId).FirstOrDefault();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to get assign project");
                return new ProjectUser();
            }
        }

        public void RemoveProject(int id)
        {
            try
            {
                var project = _context.ProjectUsers
                                .Include(m => m.ProjectRef)
                                .Where(m => m.ProjectRef.Id == id)
                                .ToList();
                _context.ProjectUsers.RemoveRange(project);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove project");
            }
        }
    }
}
