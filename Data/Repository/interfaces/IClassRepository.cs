using Data.Models;

namespace Data.Repository.interfaces
{
    internal interface IClassRepository
    {
        Task<Class> Create(Student newStudent);
        Task<Class> Update(Student newStudent);
        Task<Class> Delete(Student newStudent);
        Task<IEnumerable<Class>> GetAll();
        Task<IEnumerable<Class>> GetAllStudentsPerClass(Guid studentId);
    }
}
