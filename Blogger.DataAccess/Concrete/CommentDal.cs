using Blogger.Core.DataAccess.Concrete;
using Blogger.DataAccess.Abstract;
using Blogger.DataAccess.Context;
using Blogger.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blogger.DataAccess.Concrete
{
    public class CommentDal : EfEntityRepository<Comment, DataContext>, ICommentDal
    {
        public List<Comment> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Comments
                    .Include(c => c.Replies).ThenInclude(r => r.User)
                    .Include(c => c.User)
                    .ToList();
            }
        }

        public Comment GetCommentById(int commentId)
        {
            using (var context = new DataContext())
            {
                return context.Comments
                    .Include(c => c.Replies).ThenInclude(r => r.User)
                    .Include(c => c.User)
                    .FirstOrDefault(c => c.Id == commentId);
            }
        }

        public List<Comment> GetCommentsByUsername(string userName)
        {
            using (var context = new DataContext())
            {
                return context.Comments
                    .Include(c => c.Replies).ThenInclude(r => r.User)
                    .Include(c => c.User)
                    .Where(c => c.User.UserName == userName).ToList();
            }
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            using (var context = new DataContext())
            {
                return context.Comments
                    .Include(c => c.Replies).ThenInclude(r => r.User)
                    .Include(c => c.User)
                    .Where(c => c.User.Id == userId).ToList();
            }
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            using (var context = new DataContext())
            {
                return context.Comments
                    .Include(c => c.Replies).ThenInclude(r => r.User)
                    .Include(c => c.User)
                    .Where(c=> c.PostId == postId)
                    .ToList();
            }
        }

        public void DeleteMultiple(int userId)
        {
            using(var context = new DataContext())
            {
                context.Comments.RemoveRange(context.Comments.Where(c =>  c.UserId== userId));
                context.SaveChanges();
            }
        }
    }
}
