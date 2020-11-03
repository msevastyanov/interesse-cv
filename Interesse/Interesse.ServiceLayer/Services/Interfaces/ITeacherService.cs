using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetTeachers();
        Task<Teacher> FindTeacher(int id);
        Task<Teacher> AddTeacher(Teacher teacher);
        Task<Teacher> UpdateTeacher(Teacher teacher);
        Task<Teacher> MoveTeacherUp(int id);
        Task<Teacher> MoveTeacherDown(int id);
        Task DeleteTeacher(int id);
    }
}
