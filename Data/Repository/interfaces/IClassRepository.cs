using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IClassRepository
    {
        Class? GetById(Guid classCode);
        Class Create(Class newClass);
        Class? Update(Class newClass);
        Class? Delete(Guid newClass);
        IEnumerable<Class> GetAll();
        IEnumerable<Guid>? GetStudents(Guid classCode);
        IEnumerable<Guid>? AddStudent(Guid classCode, Guid studentId);
        IEnumerable<Guid>? RemoveStudent(Guid classCode, Guid studentId);
     

    }
}
