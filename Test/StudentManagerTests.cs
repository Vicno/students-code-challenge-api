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
    public class StudentManagerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentManager _studentManager;

        public StudentManagerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _studentManager = new StudentManager(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ValidStudent_ReturnsCreatedStudent()
        {
            // Arrange
            var newStudentDto = new StudentDto { Name = "John", LastName = "Doe" };
            var student = new Student { Id = "student123", Name = "John", LastName = "Doe" };

            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<StudentDto>())).Returns(student);
            _mockMapper.Setup(m => m.Map<StudentDto>(It.IsAny<Student>())).Returns(new StudentDto { Id = "student123", Name = "John", LastName = "Doe" });
            _mockUnitOfWork.Setup(u => u.StudentRepository.Create(It.IsAny<Student>())).Returns(student);

            // Act
            var result = _studentManager.Create(newStudentDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public void Delete_ValidStudentId_ReturnsDeletedStudent()
        {
            // Arrange
            var studentId = "student123";
            var student = new Student { Id = studentId, Name = "John", LastName = "Doe" };

            _mockUnitOfWork.Setup(u => u.StudentRepository.GetById(studentId)).Returns(student);
            _mockMapper.Setup(m => m.Map<StudentDto>(It.IsAny<Student>())).Returns(new StudentDto { Id = studentId, Name = "John", LastName = "Doe" });
            _mockUnitOfWork.Setup(u => u.StudentRepository.Delete(studentId)).Returns(student);

            // Act
            var result = _studentManager.Delete(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentId, result.Id);
            Assert.Equal("John", result.Name);
        }

        [Fact]
        public void GetAll_ReturnsAllStudents()
        {
            // Arrange
            var students = new List<Student> { new() { Id = "student123", Name = "John", LastName = "Doe" } };
            var studentDtos = new List<StudentDto> { new() { Id = "student123", Name = "John", LastName = "Doe" } };

            _mockUnitOfWork.Setup(u => u.StudentRepository.GetAll()).Returns(students);
            _mockMapper.Setup(m => m.Map<IEnumerable<StudentDto>>(It.IsAny<IEnumerable<Student>>())).Returns(studentDtos);

            // Act
            var result = _studentManager.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        // Add more tests for other methods
    }
}
