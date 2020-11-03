using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public TeacherService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _db.Teacher.Where(x => !x.IsRemoved).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<Teacher> FindTeacher(int id)
        {
            return await _db.Teacher.FindAsync(id);
        }

        public async Task<Teacher> AddTeacher(Teacher teacher)
        {
            teacher.CreatedDate = DateTime.Now;
            teacher.ChangedDate = DateTime.Now;

            if (teacher.File != null)
                teacher.Photo = await UpdateFileName(teacher.File);

            teacher.Sort = _db.Teacher.Count() + 1;

            await _db.Teacher.AddAsync(teacher);

            await _db.SaveChangesAsync();

            return await Task.FromResult(teacher);
        }

        public async Task<Teacher> UpdateTeacher(Teacher teacher)
        {
            teacher.ChangedDate = DateTime.Now;

            if (teacher.File != null)
                teacher.Photo = await UpdateFileName(teacher.File);

            _db.Update(teacher);

            await _db.SaveChangesAsync();

            return await Task.FromResult(teacher);
        }

        public async Task<Teacher> MoveTeacherUp(int id)
        {
            var teacher = await _db.Teacher.FindAsync(id);
            var anotherTeacher = await _db.Teacher.OrderByDescending(x => x.Sort).Where(x => x.Sort < teacher.Sort).FirstOrDefaultAsync();

            if (anotherTeacher == null)
                anotherTeacher = await _db.Teacher.OrderByDescending(x => x.Sort).FirstOrDefaultAsync();

            if (anotherTeacher == null)
                return await Task.FromResult(teacher);

            var currentTeacherSort = teacher.Sort;
            teacher.Sort = anotherTeacher.Sort;
            anotherTeacher.Sort = currentTeacherSort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(teacher);
        }

        public async Task<Teacher> MoveTeacherDown(int id)
        {
            var teacher = await _db.Teacher.FindAsync(id);
            var anotherTeacher = await _db.Teacher.OrderBy(x => x.Sort).Where(x => x.Sort > teacher.Sort).FirstOrDefaultAsync();

            if (anotherTeacher == null)
                anotherTeacher = await _db.Teacher.OrderBy(x => x.Sort).FirstOrDefaultAsync();

            if (anotherTeacher == null)
                return await Task.FromResult(teacher);

            var currentTeacherSort = teacher.Sort;
            teacher.Sort = anotherTeacher.Sort;
            anotherTeacher.Sort = currentTeacherSort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(teacher);
        }

        public async Task DeleteTeacher(int id)
        {
            var teacher = await _db.Teacher.FindAsync(id);

            if (teacher != null)
                teacher.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UpdateFileName(IFormFile file)
        {
            var filesPaths = await _fileService.UploadFile(new UploadFileViewModel
            {
                Folder = "teachers",
                File = file
            });

            return filesPaths.FirstOrDefault();
        }
    }
}
