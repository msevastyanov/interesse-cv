using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class TemplateImageService : ITemplateImageService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public TemplateImageService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<TemplateImage>> GetTemplateImages()
        {
            return await _db.TemplateImage.Where(x => !x.IsRemoved).ToListAsync();
        }

        public async Task<TemplateImage> FindTemplateImage(int id)
        {
            return await _db.TemplateImage.FindAsync(id);
        }

        public async Task<List<TemplateImage>> AddTemplateImages(IFormFileCollection files)
        {
            var fileNames = await GetFileNames(files);

            var templateImages = new List<TemplateImage>();

            foreach (var fileName in fileNames)
            {
                templateImages.Add(new TemplateImage
                {
                    Name = fileName,
                    CreatedDate = DateTime.Now,
                    ChangedDate = DateTime.Now,
                });
            }

            await _db.TemplateImage.AddRangeAsync(templateImages);

            await _db.SaveChangesAsync();

            return await Task.FromResult(templateImages);
        }

        public async Task DeleteTemplateImage(int id)
        {
            var templateImage = await _db.TemplateImage.FindAsync(id);

            if (templateImage != null)
                templateImage.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<List<string>> GetFileNames(IFormFileCollection files)
        {
            var filesPaths = await _fileService.UploadFiles(new UploadFilesViewModel
            {
                Folder = "templates",
                Files = files
            });

            return filesPaths;
        }
    }
}
