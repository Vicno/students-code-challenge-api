using Data.Models;
using Data.Repository.interfaces;
using JsonFlatFileDataStore;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataStore _store;
        private readonly IDocumentCollection<Student> _collection;
        public StudentRepository(string filePath) {
            _store = new DataStore(filePath);
            _collection = _store.GetCollection<Student>();
        }

        public Student Create(Student newStudent)
        {

            _collection.InsertOne(newStudent);
            return GetById(newStudent.Id);
        }

        public Student? Update(Student newStudent)
        {
            _collection.ReplaceOne(s => s.Id == newStudent.Id, newStudent);
            return GetById(newStudent.Id);
        }

        public Student? Delete(Guid studentId)
        {
            Student? removedStudent = GetById(studentId);
            _collection.DeleteOne(c => c.Id == studentId);
            return removedStudent;
        }

        public Student? GetById(Guid studentId)
        {
            return _collection.AsQueryable().FirstOrDefault(s => s.Id == studentId);
        }

        IEnumerable<Student> IStudentRepository.GetAll()
        {
            return _collection.AsQueryable().ToList();
        }
    }
}
