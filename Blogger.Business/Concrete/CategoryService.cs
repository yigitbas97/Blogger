using Blogger.Business.Abstract;
using Blogger.DataAccess.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(int categoryId)
        {
            _categoryDal.Delete(new Category { Id = categoryId });
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryDal.Get(c => c.Id == categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryDal.Get(c => c.Name == categoryName);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
