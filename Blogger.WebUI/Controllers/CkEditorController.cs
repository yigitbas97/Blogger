using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Author")]
    public class CkEditorController : Controller
    {
        private IHostingEnvironment _env;
        public CkEditorController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0)
            {
                return null;
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(_env.WebRootPath, "upload/img", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            var url = $"{"/upload/img/"}{fileName}";

            return Json(new { uploaded = true, url });
        }
    }
}
