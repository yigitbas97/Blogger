using Blogger.Business.Abstract;
using Blogger.Entities;
using Blogger.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Author")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Add()
        {
            var model = new CategoryAddViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Controlling the database exist category with same name
                var categoryNameControl = _categoryService.GetCategoryByName(model.Name);
                if (categoryNameControl != null)
                {
                    TempData["Message"] = "Bu isimde bir kategori zaten kayıtlı !";
                    TempData["MessageState"] = "danger";
                    return View(model);
                }

                var category = new Category { Name = model.Name };
                _categoryService.Add(category);

                TempData["Message"] = "Yeni kategori başarıyla eklendi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Categories", "Admin");
            }

            TempData["Message"] = "Kategori ekleme işlemi başarısız oldu !";
            TempData["MessageState"] = "danger";
            return View();
        }

        [HttpGet]
        public IActionResult Update(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category != null)
            {
                var model = new CategoryUpdateViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return View(model);
            }

            TempData["Message"] = "Kategori bulunamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Categories","Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoryUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetCategoryById(model.Id);

                if (category != null)
                {
                    // Controlling the database exist category with same name
                    var categoryNameControl = _categoryService.GetCategoryByName(model.Name);
                    if (categoryNameControl != null && categoryNameControl.Id != category.Id)
                    {
                        TempData["Message"] = "Bu kategori adı zaten kayıtlı!";
                        TempData["MessageState"] = "danger";
                        return View(model);
                    }

                    category.Name = model.Name;
                    _categoryService.Update(category);

                    TempData["Message"] = "Kategori başarıyla güncellendi !";
                    TempData["MessageState"] = "warning";
                    return RedirectToAction("Categories", "Admin");
                }

                else
                {
                    TempData["Message"] = "Kategori bulunamadı!";
                    TempData["MessageState"] = "danger";
                    return RedirectToAction("Categories", "Admin");
                }
            }

            TempData["Message"] = "Kategori güncellenirken bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Categories", "Categories");
        }

        public IActionResult Delete(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category != null)
            {
                _categoryService.Delete(categoryId);

                TempData["Message"] = "Kategori başarıyla silindi!";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Categories", "Admin");
            }
            
            TempData["Message"] = "Kategori bulunamadı!";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Categories", "Admin");
        }
    }
}
