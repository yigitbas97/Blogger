using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class PostService : IPostService
    {
        private IPostDal _postDal;
        public PostService(IPostDal postDal)
        {
            _postDal = postDal;
        }
        public void Add(Post post)
        {
            _postDal.Add(post);
        }

        public void Delete(int postId)
        {
            _postDal.Delete(new Post { Id = postId });
        }

        public List<Post> GetAll()
        {
            return _postDal.GetAll();
        }

        public Post GetPostById(int postId)
        {
            return _postDal.GetPostById(postId);
        }

        public List<Post> GetPostsByCategoryId(int categoryId)
        {
            return _postDal.GetPostsByCategoryId(categoryId);
        }

        public List<Post> GetPostsBySearchFilter(string searchFilter)
        {
            return _postDal.GetAll().Where(p =>
                    p.Category.Name.ToLower().Contains(searchFilter) ||
                    p.PostContent.ToLower().Contains(searchFilter) ||
                    p.Title.ToLower().Contains(searchFilter))
                    .ToList();
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            return _postDal.GetPostsByUserId(userId);
        }

        public List<Post> GetPostsByUsername(string userName)
        {
            return _postDal.GetPostsByUsername(userName);
        }

        public List<Post> GetTop3PostsByNumberOfClick()
        {
            return _postDal.GetTop3PostsByNumberOfClick();
        }

        public void Update(Post post)
        {
            _postDal.Update(post);
        }
    }
}
