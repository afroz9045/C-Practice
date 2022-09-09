using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
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

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddStudent([FromBody] StudentVm studentVm)
        {
            _logger.LogInformation("Adding Student details");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            return Ok(await _studentRepository.AddStudentAsync(student));
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStudents()
        {
            _logger.LogInformation("Getting Students details");
            return Ok(await _studentRepository.GetStudentsAsync());
        }

        [HttpGet("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStudentById(int studentId)
        {
            _logger.LogInformation($"Getting student details with student id: {studentId}");
            var result = Ok(await _studentRepository.GetStudentByIdAsync(studentId));
            return result;
        }

        [HttpPut("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentVm studentVm, int studentId)
        {
            _logger.LogInformation($"Updating student details with student id:{studentId}");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var result = _studentRepository.updateStudentAsync(student, studentId);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{studentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
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