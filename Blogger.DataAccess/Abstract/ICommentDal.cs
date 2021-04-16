using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface ICommentDal : IEntityRepository<Comment>
    {
        List<Comment> GetAll();
        Comment GetCommentById(int commentId);
        List<Comment> GetCommentsByUsername(string userName);
        List<Comment> GetCommentsByUserId(int userId);
        List<Comment> GetCommentsByPostId(int postId);
        void DeleteMultiple(int userId);
    }
}
