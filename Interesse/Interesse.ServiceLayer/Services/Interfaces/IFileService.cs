using Interesse.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> UploadFiles(UploadFilesViewModel filesModel);
        Task<List<string>> UploadFile(UploadFileViewModel filesModel);
        void DeleteFile(DeleteFileViewModel model);
    }
}
