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
    public class ContentPageModel : PageModel
    {
        private readonly IPageService _pageService;

        public ContentPageModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        [BindProperty]
        public new Domain.Entities.Page Page { get; set; }

        public async Task<IActionResult> OnGetAsync(string url)
        {
            Page = await _pageService.FindPage(url);

            if (Page == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
