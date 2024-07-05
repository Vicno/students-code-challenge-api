using Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IStudentManager
    {
        IEnumerable<StudentDto> GetAll();
        StudentDto GetById(Guid studentId);

        StudentDto Create(StudentDto newStudent);

        StudentDto Update(StudentDto newStudent);
        StudentDto Delete(Guid studentId);

        IEnumerable<ClassDto> GetAllClasses(Guid studentId);
    }
}
