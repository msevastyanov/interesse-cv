using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly DataContext _db;

        public VacancyService(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Vacancy>> GetVacancies()
        {
            return await _db.Vacancy.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Vacancy> FindVacancy(int id)
        {
            return await _db.Vacancy.FindAsync(id);
        }

        public async Task<Vacancy> AddVacancy(Vacancy vacancy)
        {
            vacancy.CreatedDate = DateTime.Now;
            vacancy.ChangedDate = DateTime.Now;

            await _db.Vacancy.AddAsync(vacancy);

            await _db.SaveChangesAsync();

            return await Task.FromResult(vacancy);
        }

        public async Task<Vacancy> UpdateVacancy(Vacancy vacancy)
        {
            vacancy.ChangedDate = DateTime.Now;

            _db.Update(vacancy);

            await _db.SaveChangesAsync();

            return await Task.FromResult(vacancy);
        }

        public async Task DeleteVacancy(int id)
        {
            var vacancy = await _db.Vacancy.FindAsync(id);

            if (vacancy != null)
                vacancy.IsRemoved = true;

            await _db.SaveChangesAsync();
        }
    }
}
