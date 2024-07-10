using Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IClassManager
    {
        IEnumerable<ClassDto> GetAll();
        ClassDto GetById(string classCode);

        ClassDto Create(ClassDto newClass);

        ClassDto Update(ClassDto newClass);
        ClassDto Delete(string deletedClass);
        IEnumerable<StudentDto> GetStudentsForClass(string classCode);
        ClassDto AddStudentToClass(string classCode, string studentId);
        ClassDto RemoveStudentFromClass(string classCode, string studentId);
    }
}
