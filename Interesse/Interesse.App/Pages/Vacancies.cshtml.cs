using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages
{
    public class VacanciesModel : PageModel
    {
        private readonly IVacancyService _vacancyService;

        public VacanciesModel(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public IList<Vacancy> Vacancies { get; set; }

        public async Task OnGetAsync()
        {
            Vacancies = await _vacancyService.GetVacancies();
        }
    }
}
