using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<List<Vacancy>> GetVacancies();
        Task<Vacancy> FindVacancy(int id);
        Task<Vacancy> AddVacancy(Vacancy vacancy);
        Task<Vacancy> UpdateVacancy(Vacancy vacancy);
        Task DeleteVacancy(int id);
    }
}
