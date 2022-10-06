using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Librarian,Director,Principle,HOD")]
        public async Task<ActionResult> AddStudent([FromBody] StudentVm studentVm)
        {
            _logger.LogInformation("Adding Student details");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var departmentData = await _departmentRepository.GetDepartmentsAsync();
            var studentToBeAdd = _studentService.AddStudent(student, departmentData);
            var addedStudent = studentToBeAdd != null ? await _studentRepository.AddStudentAsync(studentToBeAdd) : null;
            if (addedStudent != null)
            {
                var studentDto = _mapper.Map<Student, StudentDto>(addedStudent);
                return Ok(studentDto);
            }
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle,Professor,Lecturer,Associate Lecturer,HOD,Accountant")]
        public async Task<ActionResult> GetStudents()
        {
            _logger.LogInformation("Getting Students details");
            var studentsResult = await _studentRepository.GetStudentsAsync();
            if (studentsResult != null)
            {
                var studentDto = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(studentsResult);
                return Ok(studentDto);
            }
            return NotFound();
        }

        [HttpGet("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle,Professor,Lecturer,Associate Lecturer,HOD,Accountant")]
        public async Task<ActionResult> GetStudentById(int studentId)
        {
            _logger.LogInformation($"Getting student details with student id: {studentId}");
            var studentResult = await _studentRepository.GetStudentByIdAsync(studentId);
            if (studentResult != null)
            {
                var studentDto = _mapper.Map<Student, StudentDto>(studentResult);
                return Ok(studentDto);
            }
            return NotFound();
        }

        [HttpPut("{studentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Librarian,Director,Principle,HOD")]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentVm studentVm, int studentId)
        {
            _logger.LogInformation($"Updating student details with student id:{studentId}");
            var student = _mapper.Map<StudentVm, Student>(studentVm);
            var existingStudentRecord = await _studentRepository.GetStudentByIdAsync(studentId);
            var studentToBeUpdate = _studentService.updateStudentAsync(student, studentId, existingStudentRecord);
            var updatedStudent = studentToBeUpdate != null ? await _studentRepository.updateStudentAsync(studentToBeUpdate) : null;
            if (updatedStudent != null)
            {
                var studentDto = _mapper.Map<Student, StudentDto>(updatedStudent);
                return Ok(studentDto);
            }
            return BadRequest();
        }

        [HttpDelete("{studentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        [Authorize(Roles = "Librarian,Director,Principle")]
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