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
    public class BanController : Controller
    {
        private IBanService _banService;
        private IUserService _userService;

        public BanController(IBanService banService, IUserService userService)
        {
            _banService = banService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Add(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                TempData["Message"] = "Kullanıcı bulunamadı";
                TempData["MessageState"] = "danger";
                return RedirectToAction("Bans","Admin");
            }

            var model = new BanAddViewModel { UserId = userId };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BanAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ban = new Ban();
                ban.UserId = model.UserId;
                ban.Description = model.Description;

                _banService.Add(ban);

                TempData["Message"] = "Kısıtlama eklendi";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Bans", "Admin");
            }

            TempData["Message"] = "Bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Bans", "Admin");
        }

        public IActionResult Delete(int banId)
        {
            var ban = _banService.GetBanById(banId);

            if (ban != null)
            {
                _banService.Delete(banId);
                TempData["Message"] = "Kısıtlama başarıyla kaldırıldı.";
                TempData["MessageState"] = "warning";
                return RedirectToAction("Bans", "Admin");
            }

            TempData["Message"] = "Bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Bans", "Admin");
        }
    }
}
