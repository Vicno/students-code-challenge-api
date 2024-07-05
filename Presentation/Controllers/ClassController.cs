using AutoMapper;
using Logic.Exceptions;
using Logic.Managers.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;


namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController(IClassManager classManager, IMapper mapper) : ControllerBase
    {
        private readonly IClassManager _classManager = classManager;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ActionResult<IEnumerable<ClassDto>> GetAll()
        {
            var classes = _classManager.GetAll();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public ActionResult<ClassDto> GetById(string id)
        {
            try
            {
                var classDto = _classManager.GetById(id);
                return Ok(classDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ClassDto> Create([FromBody] ClassDto classDto)
        {
            try
            {
                var createdClass = _classManager.Create(classDto);
                return CreatedAtAction(nameof(GetById), new { id = createdClass.ClassCode }, createdClass);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ClassDto> Update(string id, [FromBody] ClassDto classDto)
        {
            if (id != classDto.ClassCode)
            {
                return BadRequest("Class code mismatch");
            }

            try
            {
                var updatedClass = _classManager.Update(classDto);
                return Ok(updatedClass);
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

        [HttpDelete("{id}")]
        public ActionResult<ClassDto> Delete(string id)
        {
            try
            {
                var deletedClass = _classManager.Delete(id);
                return Ok(deletedClass);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{classCode}/AddStudent/{studentId}")]
        public ActionResult<ClassDto> AddStudentToClass(string classCode, string studentId)
        {
            try
            {
                var updatedClass = _classManager.AddStudentToClass(classCode, studentId);
                return Ok(updatedClass);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{classCode}/RemoveStudent/{studentId}")]
        public ActionResult<ClassDto> RemoveStudentFromClass(string classCode, string studentId)
        {
            try
            {
                var updatedClass = _classManager.RemoveStudentFromClass(classCode, studentId);
                return Ok(updatedClass);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{classCode}/Students")]
        public ActionResult<IEnumerable<StudentDto>> GetStudentsForClass(string classCode)
        {
            try
            {
                var students = _classManager.GetStudentsForClass(classCode);
                return Ok(students);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
