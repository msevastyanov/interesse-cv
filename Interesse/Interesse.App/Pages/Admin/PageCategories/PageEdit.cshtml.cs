using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.PageCategories
{
    public class PageEditModel : PageModel
    {
        private readonly IPageService _pageService;

        public PageEditModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public int CategoryId { get; set; }

        [BindProperty]
        public new Domain.Entities.Page Page { get; set; }

        public async Task<IActionResult> OnGetAsync(int categoryId, int id)
        {
            CategoryId = categoryId;
            Page = id == 0 ? new Domain.Entities.Page() : await _pageService.FindPage(id);

            if (Page == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int categoryId, int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == 0)
            {
                await _pageService.AddPage(categoryId, Page);
            }
            else
            {
                await _pageService.UpdatePage(Page);
            }

            return RedirectToPage("./Pages", new { categoryId });
        }
    }
}
