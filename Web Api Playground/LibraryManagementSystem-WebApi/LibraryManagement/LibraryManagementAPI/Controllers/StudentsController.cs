using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
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
        public async Task<ActionResult> AddStudent([FromBody] StudentVm studentVm)
        {
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            return Ok(await _studentRepository.AddStudentAsync(student));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentVm studentVm, int id)
        {
            var student = _mapper.Map<StudentVm, Student>(studentVm);
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