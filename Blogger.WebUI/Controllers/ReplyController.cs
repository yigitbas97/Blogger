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
    public class ReplyController : Controller
    {
        private IReplyService _replyService;
        private ICommentService _commentService;
        private IPostService _postService;
        public ReplyController(IReplyService replyService, ICommentService commentService, IPostService postService)
        {
            _replyService = replyService;
            _commentService = commentService;
            _postService = postService;
        }

        [Authorize(Roles = "Admin,Author,Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string replyContent, int postId, int commentId)
        {
            var post = _postService.GetPostById(postId);
            var comment = _commentService.GetCommentById(commentId);

            if (String.IsNullOrEmpty(replyContent))
            {
                TempData["Message"] = "Cevap Alanı boş bırakılamaz !";
                TempData["MessageState"] = "danger";
                return RedirectToAction("Detail", "Post", new { postId = post.Id });
            }

            else if (post != null && comment != null)
            {
                var reply = new Reply
                {
                    ReplyContent = replyContent,
                    CommentId = comment.Id,
                    UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier))
                };

                _replyService.Add(reply);
               
                TempData["Message"] = "Yanıtınız eklendi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Detail", "Post", new { postId = post.Id });
            }

            TempData["Message"] = "Yanıt eklenirken bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Detail", "Post", new { postId = post.Id });
        }

        [Authorize(Roles = "Admin,Author")]
        public IActionResult Delete(int replyId)
        {
            var reply = _replyService.GetReplyById(replyId);
            if (reply != null)
            {
                _replyService.Delete(replyId);
                
                TempData["Message"] = "Yanıt başarıyla silindi !";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Replies", "Admin", new {commentId = reply.CommentId });
            }

            TempData["Message"] = "Yanıt bulunamadı!";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Posts", "Admin");
        }
    }
}
