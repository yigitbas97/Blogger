using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class CategoryUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Kategori Adı")]
        public  string Name { get; set; }
    }
}
