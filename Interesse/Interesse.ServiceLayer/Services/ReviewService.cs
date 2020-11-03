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
    public class ReviewService : IReviewService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public ReviewService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<Review>> GetReviews()
        {
            return await _db.Review.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<Review>> GetReviews(int count)
        {
            return await _db.Review.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).Take(count).ToListAsync();
        }

        public async Task<Review> FindReview(int id)
        {
            return await _db.Review.FindAsync(id);
        }

        public async Task<Review> AddReview(Review review)
        {
            review.CreatedDate = DateTime.Now;
            review.ChangedDate = DateTime.Now;
            review.IsApproved = true;

            if (review.File != null)
                review.Photo = await UpdateFileName(review.File);

            await _db.Review.AddAsync(review);

            await _db.SaveChangesAsync();

            return await Task.FromResult(review);
        }

        public async Task<Review> UpdateReview(Review review)
        {
            review.ChangedDate = DateTime.Now;

            if (review.File != null)
                review.Photo = await UpdateFileName(review.File);

            _db.Update(review);

            await _db.SaveChangesAsync();

            return await Task.FromResult(review);
        }

        public async Task DeleteReview(int id)
        {
            var review = await _db.Review.FindAsync(id);

            if (review != null)
                review.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UpdateFileName(IFormFile file)
        {
            var filesPaths = await _fileService.UploadFile(new UploadFileViewModel
            {
                Folder = "reviews",
                File = file
            });

            return filesPaths.FirstOrDefault();
        }
    }
}
