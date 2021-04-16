using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class RoleService : IRoleService
    {
        private IRoleDal _roleDal;
        public RoleService(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public void Add(Role role)
        {
            _roleDal.Add(role);
        }

        public void Delete(int roleId)
        {
            _roleDal.Delete(new Role { Id = roleId });
        }

        public List<Role> GetAll()
        {
            return _roleDal.GetAll();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleDal.GetRoleById(roleId);
        }

        public Role GetRoleByName(string roleName)
        {
            return _roleDal.GetRoleByName(roleName);
        }

        public Role GetRoleByUsername(string userName)
        {
            return _roleDal.GetRoleByUsername(userName);
        }

        public void Update(Role role)
        {
            _roleDal.Update(role);
        }
    }
}
