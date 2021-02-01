using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace EntrySheet.Web.Services
{
    public class EntityModelBuilder : IEntityModelBuilder
    {
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public EntityModelBuilder(IPasswordHasher<IdentityUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public IdentityUser GetUserEntityModel(UserViewModel model)
        {
            IdentityUser user = new IdentityUser();
            Guid guid = Guid.NewGuid();
            user.Id = guid.ToString();
            user.UserName = model.UserName;
            var hashedPasword = _passwordHasher.HashPassword(user, model.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = hashedPasword;
            user.Email = model.Email;
            return user;
        }

        public UserViewModel GetUserViewModel(IdentityUser model)
        {
            var user = new UserViewModel
            {
                Id = model.Id,
                UserName = model.UserName,
                Password = model.PasswordHash,
                Email = model.Email
            };
            return user;
        }
    }
}