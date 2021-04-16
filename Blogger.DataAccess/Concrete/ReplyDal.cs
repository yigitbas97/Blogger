using Blogger.Core.DataAccess.Concrete;
using Blogger.DataAccess.Abstract;
using Blogger.DataAccess.Context;
using Blogger.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogger.DataAccess.Concrete
{
    public class ReplyDal : EfEntityRepository<Reply, DataContext>, IReplyDal
    {
        public void DeleteMultiple(int userId)
        {
            using (var context = new DataContext())
            {
                context.Replies.RemoveRange(context.Replies.Where(r => r.UserId == userId));
                context.SaveChanges();
            }
        }

        public List<Reply> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Replies
                    .Include(r => r.User)
                    .ToList();
            }
        }

        public List<Reply> GetRepliesByUserId(int userId)
        {
            using (var context = new DataContext())
            {
                return context.Replies
                    .Include(r => r.User)
                    .Where(r => r.UserId == userId)
                    .ToList();
            }
        }

        public Reply GetReplyById(int replyId)
        {
            using (var context = new DataContext())
            {
                return context.Replies
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.Id == replyId);
            }
        }
    }
}
