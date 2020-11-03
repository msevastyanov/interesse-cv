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
    public class PageCategoryModel : PageModel
    {
        private readonly IPageCategoryService _pageCategoryService;
        private readonly IPageService _pageService;

        public PageCategoryModel(IPageCategoryService pageCategoryService, IPageService pageService)
        {
            _pageCategoryService = pageCategoryService;
            _pageService = pageService;
        }

        [BindProperty]
        public PageCategory PageCategory { get; set; }
        public IList<Domain.Entities.Page> CategoryPages { get; set; }

        public async Task<IActionResult> OnGetAsync(string url)
        {
            PageCategory = await _pageCategoryService.FindPageCategory(url);
            CategoryPages = await _pageService.GetCategoryPages(url);

            if (PageCategory == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
