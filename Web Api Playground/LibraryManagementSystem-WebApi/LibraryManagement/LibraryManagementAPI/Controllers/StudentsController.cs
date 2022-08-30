using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            return Ok(await _studentRepository.GetStudentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(int id)
        {
            var result = Ok(await _studentRepository.GetStudentByIdAsync(id));
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] StudentDto student)
        {
            return Ok(await _studentRepository.AddStudentAsync(student));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent([FromBody] Student student, int id)
        {
            var result = _studentRepository.updateStudentAsync(student, id);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = _studentRepository.DeleteStudentAsync(id);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}