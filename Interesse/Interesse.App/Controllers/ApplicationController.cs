using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interesse.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendApplication([FromBody] SendApplicationViewModel model)
        {
            try
            {
                await _applicationService.SendApplication(model);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }
    }
}
