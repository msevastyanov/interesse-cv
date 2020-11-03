using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Applications
{
    public class IndexModel : PageModel
    {
        private readonly IApplicationService _applicationService;

        public IndexModel(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IList<Application> Applications { get; set; }

        public async Task OnGetAsync()
        {
            Applications = await _applicationService.GetApplications();
        }

        public async Task<IActionResult> OnPostReadAsync(int id)
        {
            await _applicationService.MarkApplicationAsRead(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSuccessAsync(int id)
        {
            await _applicationService.MarkApplicationAsSuccessful(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _applicationService.DeleteApplication(id);

            return RedirectToPage();
        }
    }
}
