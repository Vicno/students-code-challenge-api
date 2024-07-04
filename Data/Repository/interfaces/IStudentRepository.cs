using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student newStudent);
        Student? Update(Student newStudent);
        Student? Delete(Guid studentId);
        IEnumerable<Student> GetAll();
        Student? GetById(Guid studentId);

    }
}
