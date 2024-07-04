using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student newStudent);
        Student Update(Student newStudent);
        Student Delete(Student newStudent);
        IEnumerable<Student> GetAll();
        IEnumerable<Student> GetAllClassesForStudent(Guid studentId);
        Student GetById(Guid studentId);

    }
}
