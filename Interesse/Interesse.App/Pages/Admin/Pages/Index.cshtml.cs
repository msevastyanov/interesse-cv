using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPageService _pageService;

        public IndexModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        public IList<Domain.Entities.Page> Pages { get; set; }

        public async Task OnGetAsync()
        {
            Pages = await _pageService.GetPagesWithoutCategory();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var page = await _pageService.FindPage(id);

            if (page != null)
            {
                await _pageService.DeletePage(id);
            }

            return RedirectToPage();
        }
    }
}
