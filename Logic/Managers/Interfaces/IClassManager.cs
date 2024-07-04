using Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IClassManager
    {
        IEnumerable<ClassDto> GetAll();
        ClassDto GetById(Guid classCode);

        ClassDto Create(ClassDto newClass);

        ClassDto Update(ClassDto newClass);
        ClassDto Delete(Guid deletedClass);
        IEnumerable<StudentDto> GetStudentsForClass(Guid classCode);
        ClassDto AddStudentToClass(Guid classCode, Guid studentId);
        ClassDto RemoveStudentFromClass(Guid classCode, Guid studentId);
    }
}
