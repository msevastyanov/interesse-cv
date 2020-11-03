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
    public class EditModel : PageModel
    {
        private readonly IPageCategoryService _pageCategoryService;

        public EditModel(IPageCategoryService pageCategoryService)
        {
            _pageCategoryService = pageCategoryService;
        }

        [BindProperty]
        public PageCategory PageCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PageCategory = id == 0 ? new PageCategory() : await _pageCategoryService.FindPageCategory(id);

            if (PageCategory == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == 0)
            {
                await _pageCategoryService.AddPageCategory(PageCategory);
            }
            else
            {
                await _pageCategoryService.UpdatePageCategory(PageCategory);
            }

            return RedirectToPage("./Index");
        }
    }
}
