using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class BanService : IBanService
    {
        private IBanDal _banDal;
        public BanService(IBanDal banDal)
        {
            _banDal = banDal;
        }
        public void Add(Ban ban)
        {
            _banDal.Add(ban);
        }

        public void Delete(int banId)
        {
            _banDal.Delete(new Ban { Id = banId });
        }

        public List<Ban> GetAll()
        {
            return _banDal.GetAll();
        }

        public Ban GetBanById(int banId)
        {
            return _banDal.GetBanById(banId);
        }

        public Ban GetBanByUserId(int userId)
        {
            return _banDal.GetBanByUserId(userId);
        }

        public Ban GetBanByUsername(string userName)
        {
            return _banDal.GetBanByUsername(userName);
        }

        public void Update(Ban ban)
        {
            _banDal.Update(ban);
        }
    }
}
