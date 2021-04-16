using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class PostIndexViewModel
    {
        public List<Post> Posts { get; set; }
        public int PageNumber { get; internal set; }
        public int CurrentCategory { get; internal set; }
        public int CurrentPage { get; internal set; }
    }
}
