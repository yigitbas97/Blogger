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
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private IRoleService _roleService;
        private IUserService _userService;
        private ICommentService _commentService;
        private IReplyService _replyService;

        public RoleController(IRoleService roleService, IUserService userService, ICommentService commentService, IReplyService replyService)
        {
            _roleService = roleService;
            _userService = userService;
            _commentService = commentService;
            _replyService = replyService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new RoleAddViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RoleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role();
                role.Name = model.Name;

                _roleService.Add(role);
                
                TempData["Message"] = "Rol başarıyla eklendi";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Roles", "Admin");
            }

            TempData["Message"] = "Bir hata oluştu!";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Roles", "Admin");
        }

        public IActionResult Delete(int roleId)
        {
            var role = _roleService.GetRoleById(roleId);

            if (role != null)
            {
                // UserId is foreign key in posts,comments and replies
                // You should delete this properties before deleting role
                var users = _userService.GetUsersByRoleId(role.Id);
                foreach (var user in users)
                {
                    _commentService.DeleteMultiple(user.Id);
                    _replyService.DeleteMultiple(user.Id);
                    _userService.Delete(user.Id);
                }

                _roleService.Delete(roleId);
                TempData["Message"] = "Rol başarıyla silindi";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Roles", "Admin");
            }

            TempData["Message"] = "Bir hata oluştu!";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Roles", "Admin");
        }
    }
}
