using AutoMapper;
using Data;
using Data.Models;
using Logic.Exceptions;
using Logic.Managers.Interfaces;
using Logic.Models;


namespace Logic.Managers
{
    public class StudentManager(IUnitOfWork uow, IMapper mapper) : IStudentManager
    {
        private readonly IUnitOfWork _unitOfWork = uow;
        private readonly IMapper _mapper = mapper;

        public StudentDto Create(StudentDto newStudent)
        {
            if (newStudent == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (String.IsNullOrEmpty(newStudent.Name) || String.IsNullOrEmpty(newStudent.LastName))
            {
                throw new BadRequestException("Name and lastname must be given");
            }

            Student createStudent = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = newStudent.Name,
                LastName = newStudent.LastName,
            };

            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Create(createStudent));
        }

        public StudentDto Delete(string deletedStudentId)
        {
            if (_unitOfWork.StudentRepository.GetById(deletedStudentId) == null)
            {
                throw new NotFoundException("No valid student Id found");
            }
            var allClasses = _unitOfWork.ClassRepository.GetAll();
            foreach (var classItem in allClasses)
            {
                if (classItem.Students.Contains(deletedStudentId))
                {
                    _unitOfWork.ClassRepository.RemoveStudent(classItem.ClassCode, deletedStudentId);
                }
            }

            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Delete(deletedStudentId));
        }

        public IEnumerable<StudentDto> GetAll()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(_unitOfWork.StudentRepository.GetAll());
        }

        public StudentDto GetById(string studentId)
        {
            var student = _unitOfWork.StudentRepository.GetById(studentId);
            return student == null ? throw new NotFoundException("No valid student Id found") : _mapper.Map<StudentDto>(student);
        }

        public StudentDto Update(StudentDto newStudent)
        {
            if (newStudent == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (String.IsNullOrEmpty(newStudent.Name) || String.IsNullOrEmpty(newStudent.LastName))
            {
                throw new BadRequestException("Name and lastname must be given");
            }
            Student studentToUpdate = _unitOfWork.StudentRepository.GetById(newStudent.Id) ?? throw new NotFoundException("No valid student Id found");
            studentToUpdate.Name = newStudent.Name;
            studentToUpdate.LastName = newStudent.LastName;
            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Update(studentToUpdate));
        }

        public IEnumerable<ClassDto> GetAllClasses(string studentId)
        {
            if (_unitOfWork.StudentRepository.GetById(studentId) == null)
            {
                throw new NotFoundException("No valid student Id found");
            }
            var classes = _mapper.Map<IEnumerable<ClassDto>>(_unitOfWork.ClassRepository.GetAll());
            List<ClassDto> completeClasses = [];
            foreach (ClassDto item in classes)
            {
                var memberClass = AssembleClass(item.ClassCode);
               if(memberClass.Students.Any( student => student.Id == studentId)){
                    completeClasses.Add(memberClass);
                }
                         
            }
            return completeClasses;
        }

        private ClassDto AssembleClass(string classCode)
        {
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private List<StudentDto> GetStudents(IEnumerable<string>? studentIds)
        {
            List<StudentDto> studentObjects = [];
            if (studentIds != null)
            {
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
