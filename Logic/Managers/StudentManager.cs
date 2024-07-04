using AutoMapper;
using Data;
using Data.Models;
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
            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Create(_mapper.Map<Student>(newStudent)));
        }

        public StudentDto Delete(Guid deletedStudentId)
        {
            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Delete(deletedStudentId));
        }

        public IEnumerable<StudentDto> GetAll()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(_unitOfWork.StudentRepository.GetAll());
        }

        public StudentDto GetById(Guid studentId)
        {
            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.GetById(studentId));
        }

        public StudentDto Update(StudentDto newStudent)
        {
            Student studentToUpdate = _unitOfWork.StudentRepository.GetById(newStudent.Id);
            studentToUpdate.Name = newStudent.Name;
            studentToUpdate.LastName = newStudent.LastName;
            return _mapper.Map<StudentDto>(_unitOfWork.StudentRepository.Update(studentToUpdate));
        }

        public IEnumerable<ClassDto> GetAllClasses(Guid studentId)
        {
            var classes = _mapper.Map<IEnumerable<ClassDto>>(_unitOfWork.ClassRepository.GetAll());
            List<ClassDto> completeClasses = [];
            foreach (ClassDto item in classes)
            {
                var memberClass = AssembleClass(item.ClassCode);
               if(memberClass.Students.Any( Id => Id.Equals(studentId))){
                    completeClasses.Add(memberClass);
                }
                         
            }
            return completeClasses;
        }

        private ClassDto AssembleClass(Guid classCode)
        {
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private List<StudentDto> GetStudents(IEnumerable<Guid>? studentIds)
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
