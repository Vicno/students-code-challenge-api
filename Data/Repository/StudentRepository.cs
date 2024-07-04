using Data.Models;
using Data.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository() { }

        public Task<Student> Create(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Delete(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllClassesForStudent(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student newStudent)
        {
            throw new NotImplementedException();
        }
    }
}
