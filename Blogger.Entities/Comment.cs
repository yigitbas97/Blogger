using Blogger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogger.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string CommentContent { get; set; }

        //Navgiation Properties
        public List<Reply> Replies { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        //Access User Information
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
