using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    public class StudentsController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, IStudentRepository studentRepository, IDepartmentRepository departmentRepository, IMapper mapper, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddStudent([FromBody] StudentVm studentVm)
        {
            _logger.LogInformation("Adding Student details");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var departmentData = await _departmentRepository.GetDepartmentsAsync();
            var studentToBeAdd = _studentService.AddStudent(student, departmentData);
            var addedStudent = studentToBeAdd != null ? await _studentRepository.AddStudentAsync(studentToBeAdd) : null;
            if (addedStudent != null)
                return Ok(addedStudent);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStudents()
        {
            _logger.LogInformation("Getting Students details");
            var result = await _studentRepository.GetStudentsAsync();
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStudentById(int studentId)
        {
            _logger.LogInformation($"Getting student details with student id: {studentId}");
            var result = await _studentRepository.GetStudentByIdAsync(studentId);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentVm studentVm, int studentId)
        {
            _logger.LogInformation($"Updating student details with student id:{studentId}");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var existingStudentRecord = await _studentRepository.GetStudentByIdAsync(studentId);
            var studentToBeUpdate = _studentService.updateStudentAsync(student, studentId, existingStudentRecord);
            var updatedStudent = studentToBeUpdate != null ? await _studentRepository.updateStudentAsync(studentToBeUpdate) : null;
            if (updatedStudent != null)
                return Ok(updatedStudent);
            return BadRequest();
        }

        [HttpDelete("{studentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            _logger.LogInformation($"Deleting student details with student id: {studentId} ");
            var existingStudent = await _studentRepository.GetStudentByIdAsync(studentId);
            var deletedStudent = await _studentRepository.DeleteStudentAsync(existingStudent);
            if (deletedStudent != null)
                return NoContent();
            return BadRequest();
        }
    }
}