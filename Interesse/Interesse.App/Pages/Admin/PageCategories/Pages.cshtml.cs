using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.PageCategories
{
    public class PagesModel : PageModel
    {
        private readonly IPageCategoryService _pageCategoryService;
        private readonly IPageService _pageService;

        public PagesModel(IPageCategoryService pageCategoryService, IPageService pageService)
        {
            _pageCategoryService = pageCategoryService;
            _pageService = pageService;
        }

        [BindProperty]
        public int SelectedPageId { get; set; }
        public PageCategory PageCategory { get; set; }
        public IList<Domain.Entities.Page> PagesWithoutCategory { get; set; }
        public IList<Domain.Entities.Page> CategoryPages { get; set; }

        public async Task OnGetAsync(int categoryId)
        {
            PageCategory = await _pageCategoryService.FindPageCategory(categoryId);
            PagesWithoutCategory = await _pageService.GetPagesWithoutCategory();
            CategoryPages = await _pageService.GetCategoryPages(categoryId);
        }

        public async Task<IActionResult> OnPostAsync(int categoryId)
        {
            await _pageCategoryService.AddPageToCategory(categoryId, SelectedPageId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromCategoryAsync(int pageId)
        {
            await _pageCategoryService.RemovePageFromCategory(pageId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveUpAsync(int categoryId, int id)
        {
            await _pageService.MovePageUp(categoryId, id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveDownAsync(int categoryId, int id)
        {
            await _pageService.MovePageDown(categoryId, id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int pageId)
        {
            await _pageService.DeletePage(pageId);

            return RedirectToPage();
        }
    }
}
