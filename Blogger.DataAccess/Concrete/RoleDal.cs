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
    public class RoleDal : EfEntityRepository<Role, DataContext>, IRoleDal
    {
        public List<Role> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Roles
                    .Include(r => r.Users)
                    .OrderBy(r => r.Name)
                    .ToList();
            }
        }

        public Role GetRoleById(int roleId)
        {
            using (var context = new DataContext())
            {
                return context.Roles
                    .Include(r => r.Users)
                    .FirstOrDefault(r => r.Id == roleId);
            }
        }

        public Role GetRoleByName(string roleName)
        {
            using (var context = new DataContext())
            {
                return context.Roles
                    .Include(r => r.Users)
                    .FirstOrDefault(r => r.Name == roleName);
            }
        }

        public Role GetRoleByUsername(string userName)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .FirstOrDefault(u => u.UserName == userName).Role;
            }
        }
    }
}
