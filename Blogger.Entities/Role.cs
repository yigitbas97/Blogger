using Blogger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogger.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //Navigation Properties
        public List<User> Users { get; set; }
    }
}
