using Data.Models;
using Data.Repository.interfaces;
using JsonFlatFileDataStore;
using Newtonsoft.Json;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDocumentCollection<Student> _collection;
        public StudentRepository(DataStore store) {
            
            _collection = store.GetCollection<Student>("students");
        }

        public Student Create(Student newStudent)
        {
            System.Diagnostics.Debug.WriteLine(newStudent.Id);
            _collection.InsertOne(newStudent);
            return GetById(newStudent.Id);
        }

        public Student? Update(Student newStudent)
        {
            _collection.ReplaceOne(s => s.Id == newStudent.Id, newStudent);
            return GetById(newStudent.Id);
        }

        public Student? Delete(string studentId)
        {
            Student? removedStudent = GetById(studentId);
            _collection.DeleteOne(c => c.Id == studentId);
            return removedStudent;
        }

        public Student? GetById(string studentId)
        {
            return _collection.AsQueryable().FirstOrDefault(s => s.Id == studentId);
        }

        IEnumerable<Student> IStudentRepository.GetAll()
        {
            
            return _collection.AsQueryable().ToList();
        }
    }
}
