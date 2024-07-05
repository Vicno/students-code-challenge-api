using AutoMapper;
using Logic.Exceptions;
using Logic.Managers.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudentManager studentManager, IMapper mapper) : ControllerBase
    {
        private readonly IStudentManager _studentManager = studentManager;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentManager.GetAll();
            return Ok(students);
        }

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        
        public IActionResult GetStudentById(string id)
        {
            try
            {
                var student = _studentManager.GetById(id);
                return Ok(student);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        
        public IActionResult CreateStudent([FromBody] StudentDto student)
        {
            try
            {
                var createdStudent = _studentManager.Create(student);
                return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}")]
        
        public IActionResult UpdateStudent(string id, [FromBody] StudentDto student)
        {
            try
            {
                student.Id = id;
                var updatedStudent = _studentManager.Update(student);
                return Ok(updatedStudent);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(string id)
        {
            try
            {
                var deletedStudent = _studentManager.Delete(id);
                return Ok(deletedStudent);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}/classes")]
        public IActionResult GetStudentClasses(string id)
        {
            try
            {
                var classes = _studentManager.GetAllClasses(id);
                return Ok(classes);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
