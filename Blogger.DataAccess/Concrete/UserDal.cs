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
    public class UserDal : EfEntityRepository<User, DataContext>, IUserDal
    {
        public List<User> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .OrderBy(u => u.Name)
                    .ToList();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email == email);
            }
        }

        public User GetUserById(int userId)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == userId);
            }
        }

        public User GetUserByUsername(string userName)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.UserName == userName);
            }
        }

        public List<User> GetUsersByRoleId(int roleId)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .Where(u => u.RoleId == roleId)
                    .OrderBy(u => u.Name)
                    .ToList();
            }
        }

        public List<User> GetUsersByRoleName(string roleName)
        {
            using (var context = new DataContext())
            {
                return context.Users
                    .Include(u => u.Posts)
                    .Include(u => u.Comments)
                    .Include(u => u.Replies)
                    .Include(u => u.Role)
                    .Where(u => u.Role.Name == roleName)
                    .OrderBy(u => u.Name)
                    .ToList();
            }
        }
    }
}
