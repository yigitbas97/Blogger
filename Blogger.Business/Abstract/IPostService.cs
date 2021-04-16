using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetPostById(int postId);
        List<Post> GetPostsByUsername(string userName);
        List<Post> GetPostsByUserId(int userId);
        List<Post> GetPostsByCategoryId(int categoryId);
        List<Post> GetPostsBySearchFilter(string searchFilter);
        List<Post> GetTop3PostsByNumberOfClick();
        void Add(Post post);
        void Update(Post post);
        void Delete(int postId);
    }
}
