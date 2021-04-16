using Blogger.Business.Abstract;
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
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private ICommentService _commentService;
        private IReplyService _replyService;

        public UserController(IUserService userService, IRoleService roleService, ICommentService commentService, IReplyService replyService)
        {
            _userService = userService;
            _roleService = roleService;
            _commentService = commentService;
            _replyService = replyService;
        }

        [HttpGet]
        public IActionResult UpdateRole(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                var roles = _roleService.GetAll();
                var model = new UserUpdateRoleViewModel { UserId = userId, Roles = roles, User= user };
                return View(model);
            }

            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRole(UserUpdateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserById(model.UserId);
                var role = _roleService.GetRoleById(model.RoleId);

                if (user != null && role != null)
                {
                    user.RoleId = role.Id;
                    _userService.Update(user);

                    TempData["Message"] = "Kullanıcının rolü güncellendi";
                    TempData["MessageState"] = "warning";
                    return RedirectToAction("Users", "Admin");
                }
            }

            TempData["Message"] = "Bir hata oluştu";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Users", "Admin");
        }

        public IActionResult Delete(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                //If author or admin have no post but it has comment, you should delete comments and replies manually
                var comments = _commentService.GetCommentsByUserId(userId);

                //If author or admin have no post, no comment but it has reply, you should delete replies manually.
                var replies = _replyService.GetRepliesByUserId(userId);

                if (comments.Count > 0)
                {
                    _commentService.DeleteMultiple(userId);
                }

                if (replies.Count > 0)
                {
                    _replyService.DeleteMultiple(userId);
                }

                _userService.Delete(userId); // Delete member, author or admin with him/her posts(comments,replies)


                TempData["Message"] = "Kullanıcı ve kullanıcıya ait tüm veriler silindi.";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Users", "Admin");
            }

            TempData["Message"] = "Bir hata oluştu.";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmEmail(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                _userService.ConfirmEmail(userId);

                TempData["Message"] = "Email başarıyla doğrulandı !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Users", "Admin");
            }

            TempData["Message"] = "Email doğrulanamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Users", "Admin");
        }
    }
}
