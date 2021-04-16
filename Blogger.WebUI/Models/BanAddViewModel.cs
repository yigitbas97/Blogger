using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class BanAddViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}
