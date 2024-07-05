using Xunit;
using Moq;
using AutoMapper;
using Logic.Managers;
using Data;
using Data.Models;
using Logic.Models;
using Assert = Xunit.Assert;

namespace Tests
{
    public class ClassManagerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClassManager _classManager;

        public ClassManagerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _classManager = new ClassManager(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddStudentToClass_ValidIds_ReturnsUpdatedClass()
        {
            // Arrange
            var classCode = "class123";
            var studentId = "student123";
            var student = new Student { Id = studentId, Name = "John", LastName = "Doe" };
            var classEntity = new Class { ClassCode = classCode, Title = "Math", Description = "Math Class", Students = [] };

            _mockUnitOfWork.Setup(u => u.StudentRepository.GetById(studentId)).Returns(student);
            _mockUnitOfWork.Setup(u => u.ClassRepository.GetById(classCode)).Returns(classEntity);
            _mockUnitOfWork.Setup(u => u.ClassRepository.AddStudent(classCode, studentId)).Returns(new List<string> { studentId });
            _mockMapper.Setup(m => m.Map<ClassDto>(It.IsAny<Class>())).Returns(new ClassDto { ClassCode = classCode, Title = "Math", Description = "Math Class", Students = new List<StudentDto> { new StudentDto { Id = studentId, Name = "John", LastName = "Doe" } } });

            // Act
            var result = _classManager.AddStudentToClass(classCode, studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(classCode, result.ClassCode);
            Assert.Single(result.Students);
        }

        [Fact]
        public void Create_ValidClass_ReturnsCreatedClass()
        {
            // Arrange
            var newClassDto = new ClassDto { Title = "Science", Description = "Science Class" };
            var classEntity = new Class { ClassCode = "class123", Title = "Science", Description = "Science Class", Students = [] };

            _mockMapper.Setup(m => m.Map<Class>(It.IsAny<ClassDto>())).Returns(classEntity);
            _mockMapper.Setup(m => m.Map<ClassDto>(It.IsAny<Class>())).Returns(new ClassDto { ClassCode = "class123", Title = "Science", Description = "Science Class" });
            _mockUnitOfWork.Setup(u => u.ClassRepository.Create(It.IsAny<Class>())).Returns(classEntity);

            // Act
            var result = _classManager.Create(newClassDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Science", result.Title);
            Assert.Equal("Science Class", result.Description);
        }

        [Fact]
        public void Delete_ValidClassCode_ReturnsDeletedClass()
        {
            // Arrange
            var classCode = "class123";
            var classEntity = new Class { ClassCode = classCode, Title = "Math", Description = "Math Class" };

            _mockUnitOfWork.Setup(u => u.ClassRepository.GetById(classCode)).Returns(classEntity);
            _mockMapper.Setup(m => m.Map<ClassDto>(It.IsAny<Class>())).Returns(new ClassDto { ClassCode = classCode, Title = "Math", Description = "Math Class" });
            _mockUnitOfWork.Setup(u => u.ClassRepository.Delete(classCode)).Returns(classEntity);

            // Act
            var result = _classManager.Delete(classCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(classCode, result.ClassCode);
            Assert.Equal("Math", result.Title);
        }
    }
}
