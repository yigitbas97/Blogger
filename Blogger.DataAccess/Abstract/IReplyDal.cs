using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface IReplyDal : IEntityRepository<Reply>
    {
        List<Reply> GetAll();
        List<Reply> GetRepliesByUserId(int userId);
        Reply GetReplyById(int replyId);
        void DeleteMultiple(int userId);
    }
}
