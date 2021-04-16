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
    public class BanDal : EfEntityRepository<Ban, DataContext>, IBanDal
    {
        public List<Ban> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Bans
                    .Include(b => b.User)
                    .ToList();
            }
        }

        public Ban GetBanById(int banId)
        {
            using (var context = new DataContext())
            {
                return context.Bans
                    .Include(b => b.User)
                    .FirstOrDefault(ban => ban.Id == banId);
            }
        }

        public Ban GetBanByUserId(int userId)
        {
            using (var context = new DataContext())
            {
                return context.Bans
                    .Include(b => b.User)
                    .FirstOrDefault(b => b.User.Id == userId);
            }
        }

        public Ban GetBanByUsername(string userName)
        {
            using (var context = new DataContext())
            {
                return context.Bans
                    .Include(b => b.User)
                    .FirstOrDefault(b => b.User.UserName == userName);
            }
        }
    }
}
