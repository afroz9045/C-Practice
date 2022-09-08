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
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentsController> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            _logger.LogInformation("Getting Students details");
            return Ok(await _studentRepository.GetStudentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(int studentId)
        {
            _logger.LogInformation($"Getting student details with student id: {studentId}");
            var result = Ok(await _studentRepository.GetStudentByIdAsync(studentId));
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] StudentVm studentVm)
        {
            _logger.LogInformation("Adding Student details");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            return Ok(await _studentRepository.AddStudentAsync(student));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentVm studentVm, int studentId)
        {
            _logger.LogInformation($"Updating student details with student id:{studentId}");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var result = _studentRepository.updateStudentAsync(student, studentId);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            _logger.LogInformation($"Deleting student details with student id: {studentId} ");
            var result = _studentRepository.DeleteStudentAsync(studentId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}