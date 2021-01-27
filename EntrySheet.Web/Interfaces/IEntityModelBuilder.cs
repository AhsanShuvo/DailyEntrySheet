using EntrySheet.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EntrySheet.Web.Interfaces
{
    public interface IEntityModelBuilder
    {
        IdentityUser GetUserEntityModel(UserViewModel model);
        UserViewModel GetUserViewModel(IdentityUser model);
    }
}
