using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface IBanDal : IEntityRepository<Ban>
    {
        List<Ban> GetAll();
        Ban GetBanById(int banId);
        Ban GetBanByUserId(int userId);
        Ban GetBanByUsername(string userName);
    }
}
