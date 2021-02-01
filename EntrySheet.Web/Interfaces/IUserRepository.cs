using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EntrySheet.Web.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(IdentityUser model);
        List<IdentityUser> GetFilteredUsers();
        IdentityUser GetUser(string id);
        bool AddUserRole(IdentityUserRole<string> userRole);
        List<IdentityUser> GetUsers();
        string GetUserRole(string userId);
        bool UpdateRole(IdentityUserRole<string> userRole);
        void DeleteUser(string Id);
        bool SearchUsername(string name);
    }
}
