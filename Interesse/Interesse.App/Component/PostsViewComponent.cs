using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class PostsViewComponent : ViewComponent
    {
        private readonly IPostService _postService;

        public PostsViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        bool allPosts)
        {
            var posts = allPosts ? await _postService.GetPosts() : await _postService.GetPosts(4);

            return View((posts, allPosts));
        }
    }
}
