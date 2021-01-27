using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class UserLogRepository : BaseRepository, IUserLogRepository
    {
        public UserLogRepository(ApplicationDbContext context): base(context)
        {
        }
        
        public bool AddUserLog(UserLog logDetails)
        {
            try
            {
                _context.UserLogs.Add(logDetails);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                // Need to use logger
                return false;
            }
        }

        public List<UserLog> GetUserLogs(LogHistoryFilterModel model)
        {
            try
            {
                return _context.UserLogs
                            .Include(m => m.ProjectRef)
                            .Include(m => m.UserRef)
                            .Where(item => item.EntryDate >= model.StartDate && item.EntryDate <= model.EndDate)
                            .Where(item => item.UserRef.UserName == model.UserName)
                            .Where(item => item.ProjectRef.Name == model.Project)
                            .ToList();
            }
            catch(Exception e)
            {
                return new List<UserLog>();
            }
        }
    }
}
