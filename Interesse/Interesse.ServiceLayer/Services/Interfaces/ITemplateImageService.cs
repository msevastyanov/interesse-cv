using Interesse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface ITemplateImageService
    {
        Task<List<TemplateImage>> GetTemplateImages();
        Task<TemplateImage> FindTemplateImage(int id);
        Task<List<TemplateImage>> AddTemplateImages(IFormFileCollection files);
        Task DeleteTemplateImage(int id);
    }
}
