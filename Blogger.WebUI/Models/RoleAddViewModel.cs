using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class RoleAddViewModel
    {
        [Required]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
}
