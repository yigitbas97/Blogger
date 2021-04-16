using Blogger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogger.Entities
{
    public class Reply : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string ReplyContent { get; set; }

        //Navigation Properties
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        //Access User Information
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
