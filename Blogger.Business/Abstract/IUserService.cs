using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetUserById(int userId);
        List<User> GetUsersByRoleId(int roleId);
        List<User> GetUsersByRoleName(string roleName);
        User GetUserByUsername(string userName);
        User GetUserByEmail(string email);
        User GetUserByUsernameAndEmail(string username, string email);
        void Add(User user);
        void Update(User user);
        void Delete(int userId);
        void ConfirmEmail(int userId);
    }
}
