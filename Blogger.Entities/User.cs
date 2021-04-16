using Blogger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogger.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsEmailConfirm { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        //Navigation Properties
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Reply> Replies { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
