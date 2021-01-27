using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EntrySheet.Web.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(IdentityUser model);
        List<IdentityUser> GetFilteredUsers();
        IdentityUser GetUser(string id);
    }
}
