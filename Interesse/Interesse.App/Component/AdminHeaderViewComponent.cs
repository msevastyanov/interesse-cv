using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        private readonly IApplicationService _applicationService;

        public AdminHeaderViewComponent(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var appCount = await _applicationService.GetUnreadApplicationsCount();

            return View(appCount);
        }
    }
}
