using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private ILogger<UserRepository> _logger;
        
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)
        {
            _logger = logger;
        } 

        public bool AddUser(IdentityUser model)
        {
            try
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to add user");
                return false;
            }
        }

        public List<IdentityUser> GetUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to get users");
                return new List<IdentityUser>();
            }
        }

        public List<IdentityUser> GetFilteredUsers()
        {
            try
            {
                var users =  _context.Users
                    .Where(m => !_context.ProjectUsers.Any(p => p.UserRef == m))
                    .ToList();
                users.RemoveAll(m => _context.UserRoles.Any(r => r.UserId == m.Id && r.RoleId != "User"));
                return users;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to fetch filterd users"); 
                return new List<IdentityUser>();
            }
        }

        public IdentityUser GetUser(string id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if(user != null)
                {
                    return user;
                }
                else
                {
                    return new IdentityUser();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to fetch user");
                return new IdentityUser();
            }
        }

        public bool AddUserRole(IdentityUserRole<string> userRole)
        {
            try
            {
                var res = _context.UserRoles.Add(userRole);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to add user  role");
                return false;
            }
        }

        public string GetUserRole(string userId)
        {
            try
            {
                var res = _context.UserRoles.Where(x => x.UserId == userId)
                    .Select(x => x.RoleId).FirstOrDefault();

                return res;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to fetch user role");
                return string.Empty;
            }
        }

        public bool UpdateRole(IdentityUserRole<string> userRole)
        {
            try
            {
                var roleToUpdate = _context.UserRoles.Where(m => m.UserId == userRole.UserId).FirstOrDefault();

                if (roleToUpdate == null)
                    _context.UserRoles.Add(userRole);
                else
                {
                    _context.Attach(userRole);
                    _context.Entry(userRole).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to update role.");
                return false;
            }
        }

        public void DeleteUser(string Id)
        {
            try
            {
                var user = _context.Users.Find(Id);
                _context.Users.Remove(user);
                var role = _context.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                _context.UserRoles.Remove(role);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to delete user");
            }
        }

        public bool SearchUsername(string name)
        {
            try
            {
                return _context.Users.Any(m => m.UserName == name);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to connect to the database server to search user");
                return true;
            }
        }
    }
}
