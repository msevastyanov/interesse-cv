using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<Review>> GetReviews();
        Task<List<Review>> GetReviews(int count);
        Task<Review> FindReview(int id);
        Task<Review> AddReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task DeleteReview(int id);
    }
}
