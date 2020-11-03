using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;

        public PostModel(IPostService postService)
        {
            _postService = postService;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _postService.FindPost(id);

            if (Post == null)
            {
                return RedirectToPage("./News");
            }

            return Page();
        }
    }
}
