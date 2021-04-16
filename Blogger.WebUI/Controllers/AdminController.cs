using Blogger.Business.Abstract;
using Blogger.Entities;
using Blogger.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IPostService _postService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private IReplyService _replyService;
        private IUserService _userService;
        private IRoleService _roleService;
        private IBanService _banService;

        public AdminController(
            IPostService postService, ICategoryService categoryService, ICommentService commentService,
            IReplyService replyService, IUserService userService, IRoleService roleService,
            IBanService banService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService;
            _replyService = replyService;
            _userService = userService;
            _roleService = roleService;
            _banService = banService;
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Categories()
        {
            var categories = _categoryService.GetAll();
            var model = new AdminCategoriesViewModel { Categories = categories };
            return View(model);
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Posts()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = new List<Post>();

            if (User.IsInRole("Admin"))
            {
                posts = _postService.GetAll();
            }
            else
            {
                posts = _postService.GetPostsByUserId(userId);
            }

            var model = new AdminPostsViewModel { Posts = posts };

            return View(model);
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Comments(int postId)
        {
            var post = _postService.GetPostById(postId);
            var model = new AdminCommentsViewModel { Post = post };

            return View(model);
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Replies(int commentId)
        {
            var comment = _commentService.GetCommentById(commentId);
            var post = _postService.GetPostById(comment.PostId);
            var model = new AdminRepliesViewModel { Post = post, Comment = comment };

            return View(model);
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Bans()
        {
            var bans = _banService.GetAll();
            var model = new AdminBansViewModel { Bans = bans };

            return View(model);
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Users()
        {
            List<User> users = new List<User>();

            if (User.IsInRole("Admin"))
            {
                users = _userService.GetAll();
            }

            else
            {
                users = _userService.GetUsersByRoleId(3); //Members
            }

            var model = new AdminUsersViewModel { Users = users };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            var roles = _roleService.GetAll();
            var model = new AdminRolesViewModel { Roles = roles };

            return View(model);

        }
    }
}
