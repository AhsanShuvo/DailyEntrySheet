using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EntrySheet.Web.Interfaces
{
    public interface IUserService
    {
        Task<List<Claim>> GetUserClaims();
    }
}
