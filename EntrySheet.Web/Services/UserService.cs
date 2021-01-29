using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EntrySheet.Web.Services
{
    public class UserService : IUserService
    {
        private AuthenticationStateProvider AuthenticationStateProvider;

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<Claim>> GetUserClaims()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var identities = state.User.Identities.ToList();
            var claims = identities[0].Claims.ToList();
            return claims;
        }
    }
}
