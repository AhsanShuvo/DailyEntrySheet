using EntrySheet.Web.ViewModels;
using EntrySheet.Web.Interfaces;
using FluentValidation;

namespace EntrySheet.Web.Validator
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        private IUserRepository UserRepository;

        public UserViewModelValidator(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            RuleFor(m => m.UserName)
                .NotEmpty()
                .WithMessage("Username must not be empty")
                .MinimumLength(6)
                .WithMessage("Must be at least 6 characters.")
                .Must(IsUnique)
                .WithMessage("Username must be unique");
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty();
        }

        private bool IsUnique(UserViewModel user, string name)
        {
            return !UserRepository.SearchUsername(name);
        }
    }
}
