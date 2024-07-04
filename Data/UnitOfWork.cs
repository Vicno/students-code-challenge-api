using Data.Repository;
using Data.Repository.interfaces;
using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataStore _store;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public UnitOfWork(string filePath)
        {
            _store = new DataStore(filePath);
            _classRepository = new ClassRepository(filePath);
            _studentRepository = new StudentRepository(filePath);
        }

        public IClassRepository ClassRepository
        {
            get { return _classRepository; }
        }
        public IStudentRepository StudentRepository
        {
            get { return _studentRepository; }
        }
    }
}
