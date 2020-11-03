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
    public class EditModel : PageModel
    {
        private readonly IReviewService _reviewService;

        public EditModel(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [BindProperty]
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Review = id == 0 ? new Review() : await _reviewService.FindReview(id);

            if (Review == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == 0)
            {
                await _reviewService.AddReview(Review);
            }
            else
            {
                await _reviewService.UpdateReview(Review);
            }

            return RedirectToPage("./Index");
        }
    }
}
