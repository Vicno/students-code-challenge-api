using AutoMapper;
using Data;
using Data.Models;
using Logic.Managers.Interfaces;
using Logic.Models;

namespace Logic.Managers
{
    public class ClassManager(IUnitOfWork uow, IMapper mapper) : IClassManager
    {
        private readonly IUnitOfWork _unitOfWork = uow;
        private readonly IMapper _mapper = mapper;
        public ClassDto AddStudentToClass(Guid classCode, Guid studentId)
        {
            var studentList = _unitOfWork.ClassRepository.AddStudent(classCode, studentId);
            return AssembleClass(classCode, studentList);
        }

        public ClassDto Create(ClassDto newClass)
        {
            Class creatingClass = new()
            {
                ClassCode = newClass.ClassCode,
                Title = newClass.Title,
                Description = newClass.Description,
                Students = []
            };
           return _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.Create(creatingClass));
        }

        public ClassDto Delete(Guid deletedClass)
        {
           return _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.Delete(deletedClass));
        }

        public IEnumerable<ClassDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ClassDto>>(_unitOfWork.ClassRepository.GetAll());
        }

        public ClassDto GetById(Guid classCode)
        {
            return AssembleClass(classCode);
        }

        public IEnumerable<StudentDto> GetStudentsForClass(Guid classCode)
        {
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            return GetStudents(studentList);
        }

        public ClassDto RemoveStudentFromClass(Guid classCode, Guid studentId)
        {
            var studentList = _unitOfWork.ClassRepository.RemoveStudent(classCode, studentId);
           return AssembleClass(classCode, studentList);
        }

        public ClassDto Update(ClassDto newClass)
        {
            Class classToUpdate = _unitOfWork.ClassRepository.GetById(newClass.ClassCode);
            List<Guid>classToUpdateStudents = (List<Guid>)_unitOfWork.ClassRepository.GetStudents(classToUpdate.ClassCode);
            classToUpdate.Title = newClass.Title;
            classToUpdate.Description = newClass.Description;
            classToUpdate.Students = classToUpdateStudents;
            _unitOfWork.ClassRepository.Update(classToUpdate);
            return AssembleClass(newClass.ClassCode)
;        }

        private ClassDto AssembleClass(Guid classCode)
        {
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private ClassDto AssembleClass(Guid classCode, IEnumerable<Guid> studentList)
        {
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private List<StudentDto> GetStudents(IEnumerable<Guid>? studentIds) {
            List<StudentDto> studentObjects = [];
            if (studentIds != null) {
                foreach (var student in studentIds)
                {
                    var studentObject = _unitOfWork.StudentRepository.GetById(student);
                    if (studentObject != null)
                    {
                        studentObjects.Add(_mapper.Map<StudentDto>(studentObject));
                    }
                }
                return studentObjects;
            }
            return [];
        }
    }
}
