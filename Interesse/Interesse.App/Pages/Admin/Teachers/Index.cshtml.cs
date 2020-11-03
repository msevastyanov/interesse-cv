using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interesse.App.Pages.Admin.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly ITeacherService _teacherService;

        public IndexModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public IList<Teacher> Teachers { get; set; }

        public async Task OnGetAsync()
        {
            Teachers = await _teacherService.GetTeachers();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var teacher = await _teacherService.FindTeacher(id);

            if (teacher != null)
            {
                await _teacherService.DeleteTeacher(id);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveUpAsync(int id)
        {
            await _teacherService.MoveTeacherUp(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMoveDownAsync(int id)
        {
            await _teacherService.MoveTeacherDown(id);

            return RedirectToPage();
        }
    }
}
