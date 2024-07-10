using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IClassRepository
    {
        Class? GetById(string classCode);
        Class? GetByTitle(string title);
        Class Create(Class newClass);
        Class? Update(Class newClass);
        Class? Delete(string newClass);
        IEnumerable<Class> GetAll();
        IEnumerable<string>? GetStudents(string classCode);
        IEnumerable<string>? AddStudent(string classCode, string studentId);
        IEnumerable<string>? RemoveStudent(string classCode, string studentId);
     

    }
}
