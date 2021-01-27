using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntrySheet.Web.Services
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        
        public UserRepository(ApplicationDbContext context) : base(context)
        {
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
                /// log error; 
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
                /// logger
                return new List<IdentityUser>();
            }
        }

        public List<IdentityUser> GetFilteredUsers()
        {
            try
            {
                return _context.Users
                    .Where(m => !_context.ProjectUsers.Any(p => p.UserRef == m))
                    .ToList();
            }
            catch(Exception e)
            {
                /// logger 
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
                return new IdentityUser();
            }
        }
    }
}
