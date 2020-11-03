using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Vacancies
{
    public class EditModel : PageModel
    {
        private readonly IVacancyService _vacancyService;

        public EditModel(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [BindProperty]
        public Vacancy Vacancy { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vacancy = id == 0 ? new Vacancy() : await _vacancyService.FindVacancy(id);

            if (Vacancy == null)
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
                await _vacancyService.AddVacancy(Vacancy);
            }
            else
            {
                await _vacancyService.UpdateVacancy(Vacancy);
            }

            return RedirectToPage("./Index");
        }
    }
}
