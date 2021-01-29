using EntrySheet.Web.Data;
using FluentValidation;
using System;

namespace EntrySheet.Web.Validator
{
    public class UserLogValidator : AbstractValidator<UserLog>
    {
        public UserLogValidator()
        {
            RuleFor(m => m.EntryDate).NotEmpty().LessThanOrEqualTo(DateTime.Now).WithMessage("Invalid datetime");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Description must not be empty");
            RuleFor(m => m.Hours).NotEmpty().LessThanOrEqualTo(8).WithMessage("Hours must be between 1 to 8.");
        }
    }
}
