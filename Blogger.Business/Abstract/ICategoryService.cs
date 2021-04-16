using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        void Add(Category category);
        void Update(Category category);
        void Delete(int categoryId);
    }
}
