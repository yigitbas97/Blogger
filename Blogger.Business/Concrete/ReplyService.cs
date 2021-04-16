using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class ReplyService : IReplyService
    {
        private IReplyDal _replyDal;
        public ReplyService(IReplyDal replyDal)
        {
            _replyDal = replyDal;
        }
        public void Add(Reply reply)
        {
            _replyDal.Add(reply);
        }

        public void Delete(int replyId)
        {
            _replyDal.Delete(new Reply { Id = replyId });
        }

        public void DeleteMultiple(int userId)
        {
            _replyDal.DeleteMultiple(userId);
        }

        public List<Reply> GetAll()
        {
            return _replyDal.GetAll();
        }

        public List<Reply> GetRepliesByUserId(int userId)
        {
            return _replyDal.GetRepliesByUserId(userId);
        }

        public Reply GetReplyById(int replyId)
        {
            return _replyDal.GetReplyById(replyId);
        }

        public void Update(Reply reply)
        {
            _replyDal.Update(reply);
        }
    }
}
