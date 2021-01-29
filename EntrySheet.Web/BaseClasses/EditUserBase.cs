using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class EditUserBase : ComponentBase
    {
        public UserViewModel UserInfo { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IUserRepository userRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IEntityModelBuilder EntityModelBuilder { get; set; }
        public IdentityUserRole<string> UserRole { get; set; }
        [Inject]
        public UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        public SignInManager<IdentityUser> SignInManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UserInfo = new UserViewModel();
            UserRole = new IdentityUserRole<string>();
        }

        public async Task SubmitUser()
        {
            var user = new IdentityUser { UserName = UserInfo.UserName, Email = UserInfo.Email, EmailConfirmed = true };
            await UserManager.CreateAsync(user, UserInfo.Password);
            NavigationManager.NavigateTo("/users");
            //var user = EntityModelBuilder.GetUserEntityModel(UserInfo);
            //userRepository.AddUser(user);
            //var val = UserInfo.Role.ToString();
            //UserRole.RoleId = val;
            //UserRole.UserId = user.Id;
            //userRepository.AddUserRole(UserRole);
        }
    }
}
