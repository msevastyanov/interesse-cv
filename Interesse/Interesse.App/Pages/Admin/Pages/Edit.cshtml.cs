using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly IPageService _pageService;

        public EditModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        [BindProperty]
        public new Domain.Entities.Page Page { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Page = id == 0 ? new Domain.Entities.Page() : await _pageService.FindPage(id);

            if (Page == null)
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
                await _pageService.AddPage(Page);
            }
            else
            {
                await _pageService.UpdatePage(Page);
            }

            return RedirectToPage("./Index");
        }
    }
}
