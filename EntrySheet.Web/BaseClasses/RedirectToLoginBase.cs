using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class RedirectToLoginBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject] 
        public NavigationManager Navigation { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateTask;

            if (authenticationState?.User?.Identity is null || !authenticationState.User.Identity.IsAuthenticated)
            {
                var returnUrl = Navigation.ToBaseRelativePath(Navigation.Uri);

                if (string.IsNullOrWhiteSpace(returnUrl))
                    Navigation.NavigateTo("identity/account/login", true);
                else
                    Navigation.NavigateTo($"identity/account/login?returnUrl={returnUrl}", true);
            }
        }
    }
}
