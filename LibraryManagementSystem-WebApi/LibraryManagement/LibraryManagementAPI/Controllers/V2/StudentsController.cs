using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class StudentsController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, IStudentRepository studentRepository, IMapper mapper, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
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
    }
}