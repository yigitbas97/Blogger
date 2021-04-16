using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IReplyService
    {
        List<Reply> GetAll();
        List<Reply> GetRepliesByUserId(int userId);
        Reply GetReplyById(int replyId);
        void Add(Reply reply);
        void Update(Reply reply);
        void Delete(int replyId);
        void DeleteMultiple(int userId);
    }
}
