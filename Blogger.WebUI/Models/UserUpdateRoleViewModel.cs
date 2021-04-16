using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class UserUpdateRoleViewModel
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [Display(Name = "Roller")]
        public int RoleId { get; set; }
        [Display(Name = "Roller")]
        public List<Role> Roles { get; set; }
    }
}
