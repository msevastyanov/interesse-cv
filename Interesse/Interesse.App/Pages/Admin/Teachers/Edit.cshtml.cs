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
    public class EditModel : PageModel
    {
        private readonly ITeacherService _teacherService;

        public EditModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [BindProperty]
        public Teacher Teacher { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Teacher = id == 0 ? new Teacher() : await _teacherService.FindTeacher(id);

            if (Teacher == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == 0)
            {
                await _teacherService.AddTeacher(Teacher);
            }
            else
            {
                await _teacherService.UpdateTeacher(Teacher);
            }

            return RedirectToPage("./Index");
        }
    }
}
