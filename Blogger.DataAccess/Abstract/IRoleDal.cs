using Blogger.Core.DataAccess;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Abstract
{
    public interface IRoleDal : IEntityRepository<Role>
    {
        List<Role> GetAll();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        Role GetRoleByUsername(string userName);
    }
}
