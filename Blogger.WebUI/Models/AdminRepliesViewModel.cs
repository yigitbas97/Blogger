﻿using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Models
{
    public class AdminRepliesViewModel
    {
        public Post Post { get; set; }
        public Comment Comment { get; set; }
    }
}
