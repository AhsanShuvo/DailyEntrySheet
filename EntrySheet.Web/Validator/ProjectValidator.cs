using EntrySheet.Web.Data;
using FluentValidation;

namespace EntrySheet.Web.Validator
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Project name must not be empty");
        }
    }
}
