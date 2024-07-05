using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student newStudent);
        Student? Update(Student newStudent);
        Student? Delete(string studentId);
        IEnumerable<Student> GetAll();
        Student? GetById(string studentId);

    }
}
