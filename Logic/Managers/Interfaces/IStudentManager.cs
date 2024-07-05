using Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IStudentManager
    {
        IEnumerable<StudentDto> GetAll();
        StudentDto GetById(string studentId);

        StudentDto Create(StudentDto newStudent);

        StudentDto Update(StudentDto newStudent);
        StudentDto Delete(string studentId);

        IEnumerable<ClassDto> GetAllClasses(string studentId);
    }
}
