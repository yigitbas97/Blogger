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
    public class PostDal : EfEntityRepository<Post, DataContext>, IPostDal
    {
        public List<Post> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Include(p => p.Comments).ThenInclude(c => c.User)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.User)
                    .OrderByDescending(p => p.AddedDate)
                    .ToList();
            }
        }

        public Post GetPostById(int postId)
        {
            using (var context = new DataContext())
            {
                return context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Include(p => p.Comments).ThenInclude(c => c.User)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.User)
                    .FirstOrDefault(p => p.Id == postId);
            }
        }

        public List<Post> GetPostsByCategoryId(int categoryId)
        {
            using (var context = new DataContext())
            {
                return context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Include(p => p.Comments).ThenInclude(c => c.User)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.User)
                    .Where(p => p.CategoryId == categoryId)
                    .OrderByDescending(p => p.AddedDate)
                    .ToList();
            }
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            using (var context = new DataContext())
            {
                return context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Include(p => p.Comments).ThenInclude(c => c.User)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.User)
                    .Where(p => p.User.Id == userId)
                    .OrderByDescending(p => p.AddedDate)
                    .ToList();
            }
        }

        public List<Post> GetPostsByUsername(string userName)
        {
            using (var context = new DataContext())
            {
                return context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .Include(p => p.Comments).ThenInclude(c => c.User)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.User)
                    .Where(p => p.User.UserName == userName)
                    .OrderByDescending(p => p.AddedDate)
                    .ToList();
            }
        }

        public List<Post> GetTop3PostsByNumberOfClick()
        {
            using (var context = new DataContext())
            {
                var posts = context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .OrderByDescending(p => p.NumberOfClicks)
                    .ToList();

                if (posts.Count >= 3)
                {
                    return posts.Take(3).ToList();
                }

                return posts;
            }
        }
    }
}
