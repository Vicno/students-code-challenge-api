
using Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IClassManager
    {
        IEnumerable<ClassDto> GetAll();
        ClassDto GetById(Guid classCode);

        ClassDto Create(ClassDto newClass);

        ClassDto Update(ClassDto newClass);
        ClassDto Delete(ClassDto deletedClass);
        IEnumerable<ClassDto> GetStudentsForClass(Guid classCode);
        IEnumerable<ClassDto> AddStudentToClass(Guid classCode, Guid studentId);
        IEnumerable<ClassDto> RemoveStudentFromClass(Guid classCode, Guid studentId);
    }
}
