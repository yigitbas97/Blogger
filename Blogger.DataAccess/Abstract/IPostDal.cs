using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface IPostDal : IEntityRepository<Post>
    {
        List<Post> GetAll();
        Post GetPostById(int postId);
        List<Post> GetPostsByCategoryId(int categoryId);
        List<Post> GetPostsByUsername(string userName);
        List<Post> GetPostsByUserId(int userId);
        List<Post> GetTop3PostsByNumberOfClick();
    }
}
