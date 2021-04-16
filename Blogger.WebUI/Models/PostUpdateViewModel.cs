using Blogger.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class PostUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Resim")]
        public IFormFile Image { get; set; }
        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "İçerik")]
        public string PostContent { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        public List<Category> Categories { get; set; }
    }
}
