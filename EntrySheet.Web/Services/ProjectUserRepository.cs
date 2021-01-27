using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class ProjectUserRepository : BaseRepository, IProjectUserRepository
    {
        public ProjectUserRepository(ApplicationDbContext context): base(context)
        {

        }
        public bool AddNewUser(ProjectUser model)
        {
            try
            {
                _context.ProjectUsers.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
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
                return false;
            }
        }
    }
}
