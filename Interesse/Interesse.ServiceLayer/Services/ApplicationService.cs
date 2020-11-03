using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly DataContext _db;

        public ApplicationService(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Application>> GetApplications()
        {
            return await _db.Application.Where(x => !x.IsRemoved).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<int> GetUnreadApplicationsCount()
        {
            return await _db.Application.Where(x => !x.IsRemoved && !x.IsRead).CountAsync();
        }

        public async Task<Application> SendApplication(SendApplicationViewModel model)
        {
            var app = new Application
            {
                CreatedDate = DateTime.Now,
                ChangedDate = DateTime.Now,
                Name = model.Name,
                Phone = model.Phone,
                Comment = model.Comment,
                Type = model.Type,
                IsSuccess = false,
                IsRead = false,
            };

            await _db.Application.AddAsync(app);

            await _db.SaveChangesAsync();

            return await Task.FromResult(app);
        }

        public async Task MarkApplicationAsSuccessful(int id)
        {
            var app = await _db.Application.FindAsync(id);

            if (app != null)
                app.IsSuccess = true;

            await _db.SaveChangesAsync();
        }

        public async Task MarkApplicationAsRead(int id)
        {
            var app = await _db.Application.FindAsync(id);

            if (app != null)
                app.IsRead = true;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteApplication(int id)
        {
            var app = await _db.Application.FindAsync(id);

            if (app != null)
                app.IsRemoved = true;

            await _db.SaveChangesAsync();
        }
    }
}
