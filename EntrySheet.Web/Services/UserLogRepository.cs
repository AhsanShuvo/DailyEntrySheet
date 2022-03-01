using EntrySheet.Web.ViewModels;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class UserLogRepository : BaseRepository, IUserLogRepository
    {
        private ILogger<UserLogRepository> _logger;

        public UserLogRepository(ApplicationDbContext context, ILogger<UserLogRepository> logger): base(context)
        {
            _logger = logger;
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
                _logger.LogError(e, "Failed to connect to the server to add a userlog");
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
                _logger.LogError(e, "Failed to connect to the database server to fetch user log");
                return new List<UserLog>();
            }
        }

        public void RemoveUserLog(UserLog logDetails)
        {
            try
            {
                _context.UserLogs.Remove(logDetails);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove user logs");
            }
        }

        public bool UpdateUserLog(UserLog logDetails)
        {
            try
            {
                _context.Attach(logDetails);
                _context.Entry(logDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to update user logs");
                return false;
            }
        }

        public void RemoveUserLogsByUserId(string id)
        {
            try
            {
                var userLogs = _context.UserLogs
                            .Include(m => m.UserRef)
                            .Where(m => m.UserRef.Id == id)
                            .ToList();
                _context.UserLogs.RemoveRange(userLogs);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove user logs");
            }
        }

        public void RemoveUserLogsByProjectId(int id)
        {
            try
            {
                var userLogs = _context.UserLogs
                            .Include(m => m.ProjectRef)
                            .Where(m => m.ProjectRef.Id == id)
                            .ToList();
                _context.UserLogs.RemoveRange(userLogs);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to remove user logs");
            }
        }
    }
}
