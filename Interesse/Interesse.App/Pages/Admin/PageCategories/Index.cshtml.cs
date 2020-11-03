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
    public class IndexModel : PageModel
    {
        private readonly IPageCategoryService _pageCategoryService;

        public IndexModel(IPageCategoryService pageCategoryService)
        {
            _pageCategoryService = pageCategoryService;
        }

        public IList<PageCategory> PageCategories { get; set; }

        public async Task OnGetAsync()
        {
            PageCategories = await _pageCategoryService.GetPageCategories();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var pageCategory = await _pageCategoryService.FindPageCategory(id);

            if (pageCategory != null)
            {
                await _pageCategoryService.DeletePageCategory(id);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveUpAsync(int id)
        {
            await _pageCategoryService.MovePageCategoryUp(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveDownAsync(int id)
        {
            await _pageCategoryService.MovePageCategoryDown(id);

            return RedirectToPage();
        }
    }
}
