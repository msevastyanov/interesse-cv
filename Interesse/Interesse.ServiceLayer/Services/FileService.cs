using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<string>> UploadFiles(UploadFilesViewModel filesModel)
        {
            var fileNames = new List<string>();

            var dirname = filesModel.Folder;
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            var dirPath = Path.Combine(contentRootPath, "wwwroot", "uploads", dirname);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            foreach (var file in filesModel.Files)
            {
                var fileName = Path.Combine(Path.GetRandomFileName() + Path.GetExtension(file.FileName));
                var path = Path.Combine(dirPath, fileName);
                await file.CopyToAsync(new FileStream(path, FileMode.Create));

                fileNames.Add(fileName);
            }

            return fileNames;
        }

        public async Task<List<string>> UploadFile(UploadFileViewModel filesModel)
        {
            var fileNames = new List<string>();

            var dirname = filesModel.Folder;
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            var dirPath = Path.Combine(contentRootPath, "wwwroot", "uploads", dirname);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var fileName = Path.Combine(Path.GetRandomFileName() + Path.GetExtension(filesModel.File.FileName));
            var path = Path.Combine(dirPath, fileName);
            await filesModel.File.CopyToAsync(new FileStream(path, FileMode.Create));

            fileNames.Add(fileName);

            return fileNames;
        }

        public void DeleteFile(DeleteFileViewModel model)
        {
            var dirname = model.Folder;
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            var dirPath = Path.Combine(contentRootPath, "wwwroot", "uploads", dirname);
            var path = Path.Combine(dirPath, model.FileName);

            File.Delete(path);
        }
    }
}
