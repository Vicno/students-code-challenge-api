using Data.Models;

namespace Data.Repository.interfaces
{
    public interface IStudentRepository
    {
        Task<Student> Create(Student newStudent);
        Task<Student> Update(Student newStudent);
        Task<Student> Delete(Student newStudent);
        Task<IEnumerable<Student>> GetAll();
        Task<IEnumerable<Student>> GetAllClassesForStudent(Guid studentId);

    }
}
