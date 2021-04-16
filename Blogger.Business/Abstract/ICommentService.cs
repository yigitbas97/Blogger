using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        Comment GetCommentById(int commentId);
        List<Comment> GetCommentsByUsername(string userName);
        List<Comment> GetCommentsByUserId(int userId);
        List<Comment> GetCommentsByPostId(int postId);
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(int commentId);
        void DeleteMultiple(int userId);
    }
}
