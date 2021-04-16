using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IBanService
    {
        List<Ban> GetAll();
        Ban GetBanById(int banId);
        Ban GetBanByUserId(int userId);
        Ban GetBanByUsername(string userName);
        void Add(Ban ban);
        void Update(Ban ban);
        void Delete(int banId);
    }
}
