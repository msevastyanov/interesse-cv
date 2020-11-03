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
    public class GalleryModel : PageModel
    {
        private readonly IPhotoService _photoService;

        public GalleryModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public IList<Photo> Photos { get; set; }

        public async Task OnGetAsync()
        {
            Photos = await _photoService.GetPhotos();
        }
    }
}
