using Blogger.Business.Abstract;
using Blogger.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        private IPostService _postService;
        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [Authorize(Roles = "Admin,Author,Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string postComment, int postId)
        {
            var post = _postService.GetPostById(postId);

            if (String.IsNullOrEmpty(postComment))
            {
                TempData["Message"] = "Yorum alanı boş bırakılamaz !";
                TempData["MessageState"] = "danger";
                return RedirectToAction("Detail", "Post", new { postId = post.Id });
            }

            else if (post != null)
            {
                var comment = new Comment()
                {
                    CommentContent = postComment,
                    PostId = post.Id,
                    UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier))
                };

                _commentService.Add(comment);

                TempData["Message"] = "Yorum eklendi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Detail", "Post", new { postId = post.Id });
            }

            TempData["Message"] = "Yorum eklenirken bir hata oluştu!";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Detail", "Post", new { postId = post.Id });
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Delete(int commentId)
        {
            var comment = _commentService.GetCommentById(commentId);
            if (comment != null)
            {
                _commentService.Delete(commentId);

                TempData["Message"] = "Yorum başarıyla silindi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Comments", "Admin", new { postId = comment.PostId });
            }

            TempData["Message"] = "Yorum bulunamadı !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }
    }
}
