using Blogger.Business.Abstract;
using Blogger.Entities;
using Blogger.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private IHostingEnvironment _env;
        public PostController(IPostService postService, ICategoryService categoryService, ICommentService commentService, IHostingEnvironment env)
        {
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService;
            _env = env;
        }

        public IActionResult Index(int page = 1, int categoryId = 0)
        {
            int pageSize = 4;
            var posts = new List<Post>();

            if (categoryId == 0)
            {
                posts = _postService.GetAll().ToList();
            }
            else
            {
                posts = _postService.GetPostsByCategoryId(categoryId).ToList();
            }

            var pageNumber = Convert.ToInt32(Math.Ceiling(posts.Count / (double)pageSize));
            var currentCategory = Convert.ToInt32(HttpContext.Request.Query["categoryId"]);
            var currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);

            if (currentPage == 0)
            {
                currentPage = 1;
            }

            var model = new PostIndexViewModel()
            {
                Posts = posts.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageNumber = pageNumber,
                CurrentCategory = currentCategory,
                CurrentPage = currentPage
            };

            return View(model);
        }

        public IActionResult Detail(int postId)
        {
            var post = _postService.GetPostById(postId);
            if (post != null)
            {
                //Update post's number of clicks
                post.NumberOfClicks += 1;
                _postService.Update(post);

                var model = new PostDetailViewModel { Post = post };

                return View(model);
            }

            TempData["Message"] = "Paylaşım bulunamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Index", "Post");
        }

        public IActionResult Search(string searchFilter)
        {
            if (!String.IsNullOrEmpty(searchFilter))
            {
                var posts = _postService.GetPostsBySearchFilter(searchFilter);
                
                if (posts.Count > 0)
                {
                    var model = new PostSearchViewModel
                    {
                        Posts = posts
                    };

                    return View(model);
                }

                else
                {
                    TempData["Message"] = "Aradığınız kelimeye uygun sonuç bulunamadı !";
                    TempData["MessageState"] = "warning";
                    return RedirectToAction("Index", "Post");
                }
            }

            TempData["Message"] = "Arama filtresi boş olmamalıdır !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Index", "Post");
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryService.GetAll();
            var model = new PostAddViewModel
            {
                Categories = categories,
                AddedDate = DateTime.Now,
                UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            return View(model);
        }


        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(PostAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create new Post to add database
                var post = new Post
                {
                    ImageUrl = UploadPostImage(model.Image),
                    Title = model.Title,
                    PostContent = model.PostContent,
                    NumberOfClicks = 0,
                    AddedDate = model.AddedDate,
                    CategoryId = model.CategoryId,
                    UserId = model.UserId
                };

                _postService.Add(post);

                TempData["Message"] = "Yeni paylaşım başarıyla eklendi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Posts", "Admin");
            }

            TempData["Message"] = "Paylaşım eklenirken bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }

        [Authorize(Roles = "Admin,Author")]
        public string UploadPostImage(IFormFile file)
        {
            if (file.Length <= 0)
            {
                return null;
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();
            var path = Path.Combine(_env.WebRootPath, "images/post", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }


        [Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public IActionResult Update(int postId)
        {
            var post = _postService.GetPostById(postId);
            if (post != null)
            {
                var categories = _categoryService.GetAll();

                var model = new PostUpdateViewModel
                {
                    Id = postId,
                    Title = post.Title,
                    PostContent = post.PostContent,
                    CategoryId = post.CategoryId,
                    Categories = categories,
                    ImageUrl = post.ImageUrl
                };

                return View(model);
            }

            TempData["Message"] = "Paylaşım bulunamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }


        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PostUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = _postService.GetPostById(model.Id);
                if (post != null)
                {
                    // Update post parameters
                    post.Title = model.Title;
                    post.PostContent = model.PostContent;
                    post.CategoryId = model.CategoryId;

                    // Control Model's image to update post
                    if (model.Image != null)
                    {
                        post.ImageUrl = UploadPostImage(model.Image);
                    }

                    _postService.Update(post);

                    TempData["Message"] = "Paylaşım başarıyla güncellendi";
                    TempData["MessageState"] = "warning";
                    return RedirectToAction("Posts", "Admin");
                }

                TempData["Message"] = "Paylaşım bulunamadı";
                TempData["MessageState"] = "danger";
                return RedirectToAction("Posts", "Admin");
            }

            TempData["Message"] = "Güncelleme işlemi yapılırken bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Delete(int postId)
        {
            var post = _postService.GetPostById(postId);
            if (post != null)
            {
                _postService.Delete(postId);

                TempData["Message"] = "Paylaşım silindi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Posts", "Admin");
            }

            TempData["Message"] = "Paylaşım bulunamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }
    }
}
