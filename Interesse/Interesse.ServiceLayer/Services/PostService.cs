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
    public class PostService : IPostService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public PostService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _db.Post.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<Post>> GetPosts(int count)
        {
            return await _db.Post.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).Take(count).ToListAsync();
        }

        public async Task<Post> FindPost(int id)
        {
            return await _db.Post.FindAsync(id);
        }

        public async Task<Post> AddPost(Post post)
        {
            post.CreatedDate = DateTime.Now;
            post.ChangedDate = DateTime.Now;

            if (post.File != null)
                post.PreviewImage = await UpdateFileName(post.File);

            await _db.Post.AddAsync(post);

            await _db.SaveChangesAsync();

            return await Task.FromResult(post);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            post.ChangedDate = DateTime.Now;

            if (post.File != null)
                post.PreviewImage = await UpdateFileName(post.File);

            _db.Update(post);

            await _db.SaveChangesAsync();

            return await Task.FromResult(post);
        }

        public async Task DeletePost(int id)
        {
            var post = await _db.Post.FindAsync(id);

            if (post != null)
                post.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UpdateFileName(IFormFile file)
        {
            var filesPaths = await _fileService.UploadFile(new UploadFileViewModel
            {
                Folder = "news",
                File = file
            });

            return filesPaths.FirstOrDefault();
        }
    }
}
