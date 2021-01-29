using EntrySheet.Domain.ViewModels;
using FluentValidation;

namespace EntrySheet.Web.Validator
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(m => m.UserName).NotEmpty().WithMessage("Username must not be empty");
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
