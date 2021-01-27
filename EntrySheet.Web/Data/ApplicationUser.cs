using Microsoft.AspNetCore.Identity;

namespace EntrySheet.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public IdentityRole Role { get; set; }
    }
}
