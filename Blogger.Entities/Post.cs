using Blogger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogger.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostContent { get; set; }
        public int NumberOfClicks { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }

        //Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }

        //Access User Information
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
