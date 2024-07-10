﻿using AutoMapper;
using Data;
using Data.Models;
using Logic.Exceptions;
using Logic.Managers.Interfaces;
using Logic.Models;

namespace Logic.Managers
{
    public class ClassManager(IUnitOfWork uow, IMapper mapper) : IClassManager
    {
        private readonly IUnitOfWork _unitOfWork = uow;
        private readonly IMapper _mapper = mapper;
        public ClassDto AddStudentToClass(string classCode, string studentId)
        {
            if (_unitOfWork.StudentRepository.GetById(studentId) == null)
            {
                throw new NotFoundException("No Valid Student Id found");
            };

            if (_unitOfWork.ClassRepository.GetById(classCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };
            var studentList = _unitOfWork.ClassRepository.AddStudent(classCode, studentId);
            return AssembleClass(classCode, studentList);
        }

        public ClassDto Create(ClassDto newClass)
        {
            if (newClass == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (String.IsNullOrEmpty(newClass.Title) || String.IsNullOrEmpty(newClass.Description))
            {
                throw new BadRequestException("Title and Description Fields cannot be empty");
            }

            if (_unitOfWork.ClassRepository.GetByTitle(newClass.Title) != null)
            {
                throw new BadRequestException("Class with the same title already exists");
            }

            Class creatingClass = new()
            {
                ClassCode = Guid.NewGuid().ToString(),
                Title = newClass.Title,
                Description = newClass.Description,
                Students = []
            };
            return _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.Create(creatingClass));
        }

        public ClassDto Delete(string classCode)
        {
            if (_unitOfWork.ClassRepository.GetById(classCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };
            return _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.Delete(classCode));
        }

        public IEnumerable<ClassDto> GetAll()
        {
            var classes = _mapper.Map<IEnumerable<ClassDto>>(_unitOfWork.ClassRepository.GetAll());
            List<ClassDto> completeClasses = []; 
            foreach(ClassDto item in classes)
            {
                completeClasses.Add(AssembleClass(item.ClassCode));
            }
            return completeClasses;
        }

        public ClassDto GetById(string classCode)
        {
            if (_unitOfWork.ClassRepository.GetById(classCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };
            return AssembleClass(classCode);
        }

        public IEnumerable<StudentDto> GetStudentsForClass(string classCode)
        {
            if (_unitOfWork.ClassRepository.GetById(classCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            return GetStudents(studentList);
        }

        public ClassDto RemoveStudentFromClass(string classCode, string studentId)
        {
            if (_unitOfWork.ClassRepository.GetById(classCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };

            
            var studentList = _unitOfWork.ClassRepository.RemoveStudent(classCode, studentId);
           return AssembleClass(classCode, studentList);
        }

        public ClassDto Update(ClassDto newClass)
        {
            if (newClass == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (_unitOfWork.ClassRepository.GetById(newClass.ClassCode) == null)
            {
                throw new NotFoundException("No Valid class code found");
            };
            if (String.IsNullOrEmpty(newClass.Title) || String.IsNullOrEmpty(newClass.Description))
            {
                throw new BadRequestException("Title and Description Fields cannot be empty");
            }
            Class classToUpdate = _unitOfWork.ClassRepository.GetById(newClass.ClassCode);
            List<string> classToUpdateStudents = (List<string>)_unitOfWork.ClassRepository.GetStudents(classToUpdate.ClassCode);
            classToUpdate.Title = newClass.Title;
            classToUpdate.Description = newClass.Description;
            classToUpdate.Students = classToUpdateStudents;
            _unitOfWork.ClassRepository.Update(classToUpdate);
            return AssembleClass(newClass.ClassCode)
;        }

        private ClassDto AssembleClass(string classCode)
        {
            var studentList = _unitOfWork.ClassRepository.GetStudents(classCode);
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private ClassDto AssembleClass(string classCode, IEnumerable<string> studentList)
        {
            var studentObjects = GetStudents(studentList);

            var result = _mapper.Map<ClassDto>(_unitOfWork.ClassRepository.GetById(classCode));
            result.Students = studentObjects;
            return result;
        }

        private List<StudentDto> GetStudents(IEnumerable<string>? studentIds) {
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
