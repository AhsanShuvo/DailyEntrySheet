using EntrySheet.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EntrySheet.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
