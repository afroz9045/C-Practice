using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{departmentName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentByName(string departmentName)
        {
            if (departmentName == null)
            {
                return BadRequest(new ArgumentNullException(nameof(departmentName)));
            }
            _logger.LogInformation($"Getting Department details by department name: {departmentName}");
            var departmentResult = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            if (departmentResult != null)
                return Ok(departmentResult);
            return BadRequest();
        }
    }
}