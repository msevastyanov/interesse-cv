using Interesse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotos();
        Task<Photo> FindPhoto(int id);
        Task<List<Photo>> AddPhotos(IFormFileCollection files);
        Task DeletePhoto(int id);
    }
}
