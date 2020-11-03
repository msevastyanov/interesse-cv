using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly IReviewService _reviewService;

        public IndexModel(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IList<Review> Reviews { get; set; }

        public async Task OnGetAsync()
        {
            Reviews = await _reviewService.GetReviews();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var review = await _reviewService.FindReview(id);

            if (review != null)
            {
                await _reviewService.DeleteReview(id);
            }

            return RedirectToPage();
        }
    }
}
