using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class ReviewsViewComponent : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public ReviewsViewComponent(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        bool allReviews)
        {
            var posts = allReviews ? await _reviewService.GetReviews() : await _reviewService.GetReviews(4);

            return View((posts, allReviews));
        }
    }
}
