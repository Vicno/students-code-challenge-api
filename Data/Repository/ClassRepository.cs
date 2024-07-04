using Data.Models;
using Data.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ClassRepository : IClassRepository
    {
        Task<Class> IClassRepository.Create(Student newStudent)
        {
            throw new NotImplementedException();
        }

        Task<Class> IClassRepository.Delete(Student newStudent)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Class>> IClassRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Class>> IClassRepository.GetAllStudentsPerClass(Guid studentId)
        {
            throw new NotImplementedException();
        }

        Task<Class> IClassRepository.Update(Student newStudent)
        {
            throw new NotImplementedException();
        }
    }
}
