using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IRoleService
    {
        List<Role> GetAll();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        Role GetRoleByUsername(string userName);
        void Add(Role role);
        void Update(Role role);
        void Delete(int roleId);
    }
}
