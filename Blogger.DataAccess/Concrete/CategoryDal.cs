using Blogger.Core.DataAccess;
using Blogger.Core.DataAccess.Concrete;
using Blogger.DataAccess.Abstract;
using Blogger.DataAccess.Context;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.DataAccess.Concrete
{
    public class CategoryDal : EfEntityRepository<Category,DataContext>, ICategoryDal
    {
    }
}
