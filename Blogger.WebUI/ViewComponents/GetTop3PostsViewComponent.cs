using Blogger.Business.Abstract;
using Blogger.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.WebUI.ViewComponents
{
    public class GetTop3PostsViewComponent : ViewComponent
    {
        private IPostService _postService;
        public GetTop3PostsViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public ViewViewComponentResult Invoke()
        {
            var posts = _postService.GetTop3PostsByNumberOfClick();

            var model = new GetTop3PostsViewModel()
            {
                Posts =posts
            };

            return View(model);
        }
    }
}
