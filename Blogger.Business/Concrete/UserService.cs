using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void ConfirmEmail(int userId)
        {
            var user = _userDal.GetUserById(userId);

            user.IsEmailConfirm = true;
            _userDal.Update(user);
        }

        public void Delete(int userId)
        {
            _userDal.Delete(new User { Id = userId });
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetUserByEmail(string email)
        {
            return _userDal.GetUserByEmail(email);
        }

        public User GetUserById(int userId)
        {
            return _userDal.GetUserById(userId);
        }

        public User GetUserByUsername(string userName)
        {
            return _userDal.GetUserByUsername(userName);
        }

        public User GetUserByUsernameAndEmail(string username, string email)
        {
            return _userDal.Get(u => u.UserName == username && u.Email == email);
        }

        public List<User> GetUsersByRoleId(int roleId)
        {
            return _userDal.GetUsersByRoleId(roleId);
        }

        public List<User> GetUsersByRoleName(string roleName)
        {
            return _userDal.GetUsersByRoleName(roleName);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
