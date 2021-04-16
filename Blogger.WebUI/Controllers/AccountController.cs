using Blogger.Business.Abstract;
using Blogger.Entities;
using Blogger.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IUserService _userService;
        private IBanService _banService;
        public AccountController(IAccountService accountService, IUserService userService, IBanService banService )
        {
            _accountService = accountService;
            _userService = userService;
            _banService = banService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _accountService.Login(model.Username, model.Password);
                var ban = _banService.GetBanByUsername(model.Username);
                
                if (user == null)
                {
                    TempData["Message"] = "Giriş yapılamadı. Lütfen kullanıcı adınızı ve şifrenizi kontrol ediniz.";
                    TempData["MessageState"] = "danger";

                    return RedirectToAction("Login", "Account");
                }

                else if (!user.IsEmailConfirm)
                {
                    TempData["Message"] = "Giriş yapabilmek için önce emailinizi doğrulamalısınız !";
                    TempData["MessageState"] = "warning";

                    return RedirectToAction("EmailValidation", "Account", new { username = user.UserName, email = user.Email });
                }

                else if (ban != null)
                {
                    TempData["Message"] = "Ban listesine alındınız giriş yapamazsınız !";
                    TempData["MessageState"] = "danger";

                    return RedirectToAction("Index", "Post");
                }

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                },
                CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Post");
            }

            TempData["Message"] = "Giriş yapılamadı !";
            TempData["MessageState"] = "danger";

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.UserExistByUsername(model.Username))
                {
                    TempData["Message"] = "Bu kullanıcı adı daha önce alınmış !";
                    TempData["MessageState"] = "danger";

                    return View(model);
                }

                if (_accountService.UserExistByEmail(model.Email))
                {
                    TempData["Message"] = "Bu email adresi zaten kayıtlı !";
                    TempData["MessageState"] = "danger";

                    return View(model);
                }

                var user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Username
                };

                _accountService.Register(user, model.Password);

                TempData["Message"] = "Kayıt işleminiz gerçekleşti ! Lütfen mailinizi doğrulayınız.";
                TempData["MessageState"] = "warning";

                return RedirectToAction("EmailValidation", "Account", new { username = model.Username, email = model.Email });
            }

            TempData["Message"] = "Kayıt işlemi gerçekleştirilemedi !";
            TempData["MessageState"] = "danger";

            return View(model);
        }

        [HttpGet]
        public IActionResult EmailValidation(string username, string email)
        {
            bool validationCode = String.IsNullOrEmpty(HttpContext.Session.GetString(username));

            if (validationCode)
            {
                var code = _accountService.GenerateCode();
                HttpContext.Session.SetString(username, code);

                var description = "Bu kodu mail doğrulama sayfasına girerek mailinizi doğrulayabilirsiniz.";
                _accountService.SendCodeWithEmail(email, code, description);
            }

            var model = new EmailValidationViewModel { Username = username, Email = email };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmailValidation(EmailValidationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByUsername(model.Username);

                if (user == null)
                {
                    TempData["Message"] = "Böyle bir kullanıcı bulunamadı !";
                    TempData["MessageState"] = "danger";

                    return RedirectToAction("Login", "Account");
                }

                if (HttpContext.Session.GetString(model.Username) == model.Code)
                {
                    user.IsEmailConfirm = true;
                    _userService.Update(user);

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.Name)
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    TempData["Message"] = "Mail doğrulama işlemi başarıyla gerçekleşti !";
                    TempData["MessageState"] = "warning";

                    return RedirectToAction("Index", "Post");
                }
            }

            TempData["Message"] = "Mail doğrulama işlemi gerçekleştirilemedi !";
            TempData["MessageState"] = "danger";

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByUsernameAndEmail(model.Username, model.Email);

                if (user != null)
                {
                    //Code is temp password for user.
                    var code = _accountService.GenerateCode(); 
                    byte[] passwordHash, passwordSalt;
                    _accountService.CreatePasswordHash(code, out passwordHash, out passwordSalt);

                    //Generating users new password information
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    _userService.Update(user);
                    
                    //Give information for user
                    var description = "Şifremi unuttum işleminize başvurunuzdan dolayı şifreniz değiştirildi." +
                        "\n Size gönderdiğimiz geçici şifreyle hesabınıza tekrar erişim sağlayabilirsiniz." +
                        "\nGüvenliğiniz için lütfen giriş yaptıktan sonra şifrenizi tekrar değiştiriniz.";

                    _accountService.SendCodeWithEmail(user.Email, code, description);

                    TempData["Message"] = "Şifreniz başarıyla değiştirildi! Lütfen mailinizi kontrol ediniz.";
                    TempData["MessageState"] = "warning";

                    return RedirectToAction("Login", "Account");
                }

                else
                {
                    TempData["Message"] = "Girdiğiniz bilgilere ait kullanıcı bulunamadı !";
                    TempData["MessageState"] = "danger";

                    return RedirectToAction("Login", "Account");
                }
            }

            TempData["Message"] = "İşlem gerçekleştirilemedi ! Lütfen sonra tekrar deneyiniz.";
            TempData["MessageState"] = "danger";

            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles ="Admin,Author,Member")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index","Post");
        }

        [Authorize(Roles = "Admin,Author,Member")]
        public IActionResult Profile()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetUserById(userId);

            var model = new AccountProfileViewModel { UserId = userId, User = user };

            return View(model);
        }

        [Authorize(Roles = "Admin,Author,Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(AccountProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserById(model.UserId);

                if (user != null)
                {
                    var result = _accountService.ChangePassword(user, model.CurrentPassword, model.NewPassword);
                    
                    if (result)
                    {
                        TempData["Message"] = "Şifreniz başarıyla değiştirildi.";
                        TempData["MessageState"] = "warning";
                        return RedirectToAction("Profile", "Account");
                    }
                }
            }

            TempData["Message"] = "Bir hata oluştu !";
            TempData["MessageState"] = "danger";
            return RedirectToAction("Profile", "Account");
        }
    }
}
