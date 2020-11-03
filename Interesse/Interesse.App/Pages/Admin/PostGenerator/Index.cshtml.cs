using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.PostGenerator
{
    public class IndexModel : PageModel
    {
        private readonly ITemplateImageService _templateImageService;

        public IndexModel(ITemplateImageService templateImageService)
        {
            _templateImageService = templateImageService;
        }

        [BindProperty]
        public IFormFileCollection Files { get; set; }
        public IList<TemplateImage> TemplateImages { get; set; }

        public async Task OnGetAsync()
        {
            TemplateImages = await _templateImageService.GetTemplateImages();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _templateImageService.AddTemplateImages(Files);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var templateImage = await _templateImageService.FindTemplateImage(id);

            if (templateImage != null)
            {
                await _templateImageService.DeleteTemplateImage(id);
            }

            return RedirectToPage();
        }
    }
}
