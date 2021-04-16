using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentDal _commentDal;
        public CommentService(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public void Add(Comment comment)
        {
            _commentDal.Add(comment);
        }

        public void Delete(int commentId)
        {
            _commentDal.Delete(new Comment { Id = commentId });
        }

        public void DeleteMultiple(int userId)
        {
            _commentDal.DeleteMultiple(userId);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll();
        }

        public Comment GetCommentById(int commentId)
        {
            return _commentDal.GetCommentById(commentId);
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            return _commentDal.GetCommentsByPostId(postId);
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            return _commentDal.GetCommentsByUserId(userId);
        }

        public List<Comment> GetCommentsByUsername(string userName)
        {
            return _commentDal.GetCommentsByUsername(userName);
        }

        public void Update(Comment comment)
        {
            _commentDal.Update(comment);
        }
    }
}
