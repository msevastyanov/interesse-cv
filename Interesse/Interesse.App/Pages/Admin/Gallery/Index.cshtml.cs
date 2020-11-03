using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Gallery
{
    public class IndexModel : PageModel
    {
        private readonly IPhotoService _photoService;

        public IndexModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [BindProperty]
        public IFormFileCollection Files { get; set; }
        public IList<Photo> Photos { get; set; }

        public async Task OnGetAsync()
        {
            Photos = await _photoService.GetPhotos();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _photoService.AddPhotos(Files);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var photo = await _photoService.FindPhoto(id);

            if (photo != null)
            {
                await _photoService.DeletePhoto(id);
            }

            return RedirectToPage();
        }
    }
}
