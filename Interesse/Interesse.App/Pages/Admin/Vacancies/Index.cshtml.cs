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
    public class IndexModel : PageModel
    {
        private readonly IVacancyService _vacancyService;

        public IndexModel(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public IList<Vacancy> Vacancies { get; set; }

        public async Task OnGetAsync()
        {
            Vacancies = await _vacancyService.GetVacancies();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var vacancy = await _vacancyService.FindVacancy(id);

            if (vacancy != null)
            {
                await _vacancyService.DeleteVacancy(id);
            }

            return RedirectToPage();
        }
    }
}
