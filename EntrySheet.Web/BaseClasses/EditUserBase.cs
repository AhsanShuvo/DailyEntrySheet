using EntrySheet.Domain.Enums;
using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class EditUserBase : ComponentBase
    {
        public UserViewModel UserInfo { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IUserRepository UserRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
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

            if(Id == null)
            {
                Title = "Create a new user";
                ButtonText = "Add";
            }
            else
            {
                var user = UserRepository.GetUser(Id);
                UserInfo.Email = user.Email;
                UserInfo.UserName = user.UserName;
                UserInfo.Password = user.PasswordHash;
                var role = UserRepository.GetUserRole(Id);
                Enum.TryParse(role, out Role userRole);
                UserInfo.Role = userRole;

                Title = "Edit User Information";
                ButtonText = "Update";

            }
        }

        public async Task SubmitUser()
        {
            if(Id == null)
            {
                var user = new IdentityUser {UserName = UserInfo.Email, Email = UserInfo.Email, EmailConfirmed = true};
                await UserManager.CreateAsync(user, UserInfo.Password);
                await ConfirmUser(user.Email);
                UserRole.UserId = user.Id;
                var role = UserInfo.Role.ToString();
                UserRole.RoleId = role;
                UserRepository.AddUserRole(UserRole);
            }
            else
            {
                UserRole.UserId = Id;
                UserRole.RoleId = UserInfo.Role.ToString();
                UserRepository.UpdateRole(UserRole);
            }
            
            NavigationManager.NavigateTo("/users");
        }

        public async Task ConfirmUser(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await UserManager.ConfirmEmailAsync(user, code);    
        }
    }
}
