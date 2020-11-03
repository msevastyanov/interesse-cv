using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<List<Application>> GetApplications();
        Task<int> GetUnreadApplicationsCount();
        Task<Application> SendApplication(SendApplicationViewModel model);
        Task MarkApplicationAsSuccessful(int id);
        Task MarkApplicationAsRead(int id);
        Task DeleteApplication(int id);
    }
}
