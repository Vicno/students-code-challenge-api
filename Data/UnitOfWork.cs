using Data.Models;
using Data.Repository;
using Data.Repository.interfaces;
using JsonFlatFileDataStore;
using System;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly string filePath = "../Data/Database/database.json";
        private readonly DataStore _store;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public UnitOfWork()
        {
            System.Diagnostics.Debug.WriteLine($"Using JSON file path: {filePath}");
            _store = new DataStore(filePath);

            var initialData = _store.GetCollection<Student>().AsQueryable().ToList();
            System.Diagnostics.Debug.WriteLine($"Initial student data count: {initialData.Count}");

            _classRepository = new ClassRepository(_store);
            _studentRepository = new StudentRepository(_store);
        }

        public IClassRepository ClassRepository => _classRepository;
        public IStudentRepository StudentRepository => _studentRepository;
    }
}
