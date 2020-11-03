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
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public PhotoService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<Photo>> GetPhotos()
        {
            return await _db.Photo.Where(x => !x.IsRemoved).ToListAsync();
        }

        public async Task<Photo> FindPhoto(int id)
        {
            return await _db.Photo.FindAsync(id);
        }

        public async Task<List<Photo>> AddPhotos(IFormFileCollection files)
        {
            var fileNames = await GetFileNames(files);

            var photos = new List<Photo>();

            foreach (var fileName in fileNames)
            {
                photos.Add(new Photo
                {
                    Name = fileName,
                    CreatedDate = DateTime.Now,
                    ChangedDate = DateTime.Now,
                });
            }

            await _db.Photo.AddRangeAsync(photos);

            await _db.SaveChangesAsync();

            return await Task.FromResult(photos);
        }

        public async Task DeletePhoto(int id)
        {
            var photo = await _db.Photo.FindAsync(id);

            if (photo != null)
                photo.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<List<string>> GetFileNames(IFormFileCollection files)
        {
            var filesPaths = await _fileService.UploadFiles(new UploadFilesViewModel
            {
                Folder = "gallery",
                Files = files
            });

            return filesPaths;
        }
    }
}
