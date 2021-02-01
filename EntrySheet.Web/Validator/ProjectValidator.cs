using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using FluentValidation;
using System;

namespace EntrySheet.Web.Validator
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        private IProjectRepository ProjectRepository;
        public ProjectValidator(IProjectRepository projectRepository)
        {
            ProjectRepository = projectRepository;
            RuleFor(m => m.Name).NotEmpty().WithMessage("Project name must not be empty");
            RuleFor(m => m.Name).Must(IsUnique).WithMessage("Project name must be unique");
        }

        private bool IsUnique(Project project, string name)
        {
            return !ProjectRepository.SearchProjectName(name);
        }
    }
}
