using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class TeachersViewComponent : ViewComponent
    {
        private readonly ITeacherService _teacherService;

        public TeachersViewComponent(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = await _teacherService.GetTeachers();

            return View(teachers);
        }
    }
}
