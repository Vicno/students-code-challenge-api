using Data.Models;
using Data.Repository.interfaces;
using JsonFlatFileDataStore;

namespace Data.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataStore _store;
        private readonly IDocumentCollection<Class> _collection;

        public ClassRepository(string filePath)
        {
            _store = new DataStore(filePath);
            _collection = _store.GetCollection<Class>();
        }

        public Class Create(Class newClass)
        {
            _collection.InsertOne(newClass);
            return GetById(newClass.ClassCode);
        }

        public Class? Delete(Guid classCode)
        {
            Class? removedClass = GetById(classCode);
            _collection.DeleteOne(c => c.ClassCode == classCode);
            return removedClass;
        }

        public IEnumerable<Class> GetAll()
        {
            return _collection.AsQueryable().ToList();
        }

        public Class? GetById(Guid classCode)
        {
            return _collection.AsQueryable().FirstOrDefault(c => c.ClassCode == classCode);
        }

        public Class? Update(Class classItem)
        {
            _collection.ReplaceOne(c => c.ClassCode == classItem.ClassCode , classItem);
            return GetById(classItem.ClassCode);
        }

        public IEnumerable<Guid>? GetStudents(Guid classCode)
        {
            var classItem = GetById(classCode);
            return classItem?.Students ?? [];
        }

        public IEnumerable<Guid>? AddStudent(Guid classCode, Guid studentId)
        {
            var classItem = GetById(classCode);
            if (classItem != null)
            {
                classItem.Students.Add(studentId);
                Update(classItem);
            }

            return classItem?.Students;
        }

        public IEnumerable<Guid>? RemoveStudent(Guid classCode, Guid studentId)
        {
            var classItem = GetById(classCode);
            if (classItem != null)
            {
                classItem.Students.Remove(studentId);
                Update(classItem);
            }
            return classItem.Students;
        }
      

    }
}
