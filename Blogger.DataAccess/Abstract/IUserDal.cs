using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<User> GetAll();
        User GetUserById(int userId);
        User GetUserByUsername(string userName);
        User GetUserByEmail(string email);
        List<User> GetUsersByRoleId(int roleId);
        List<User> GetUsersByRoleName(string roleName);
    }
}
