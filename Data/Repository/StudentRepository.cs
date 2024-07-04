using Data.Models;
using Data.Repository.interfaces;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository() { }

        public Student Create(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public Student Delete(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllClassesForStudent(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Student GetById(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Student Update(Student newStudent)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Student> IStudentRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
