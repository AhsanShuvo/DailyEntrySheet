using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EntrySheet.Web.BaseClasses
{
    public class CreateUserBase : ComponentBase
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

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UserInfo = new UserViewModel();
        }

        public void SubmitUser()
        {
            var user = EntityModelBuilder.GetUserEntityModel(UserInfo);
            var res = userRepository.AddUser(user);
            NavigationManager.NavigateTo("users", true);
        }
    }
}
