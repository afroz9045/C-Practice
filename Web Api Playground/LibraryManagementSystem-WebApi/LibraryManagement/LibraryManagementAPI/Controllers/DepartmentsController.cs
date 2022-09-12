using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
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

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentVm departmentVm)
        {
            _logger.LogInformation("Adding Department");
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            var addedDepartment = await _departmentService.AddDepartmentAsync(department);
            if (addedDepartment != null)
                return Ok(addedDepartment);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartments()
        {
            _logger.LogInformation("Getting Departments details");
            var departments = await _departmentService.GetDepartmentsAsync();
            if (departments != null)
                return Ok(departments);
            return NotFound();
        }

        [HttpGet("{departmentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentById(short departmentId)
        {
            _logger.LogInformation($"Getting Department details by id: {departmentId}");
            var departmentResult = await _departmentService.GetDepartmentByIdAsync(departmentId);
            if (departmentResult != null)
                return Ok(departmentResult);
            return BadRequest();
        }

        [HttpGet("{departmentName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentByName(string departmentName)
        {
            _logger.LogInformation($"Getting Department details by department name: {departmentName}");
            var departmentResult = await _departmentService.GetDepartmentByNameAsync(departmentName);
            if (departmentResult != null)
                return Ok(departmentResult);
            return BadRequest();
        }

        [HttpPut("{departmentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentVm departmentVm, short departmentId)
        {
            _logger.LogInformation($"Updating Department with department id: {departmentId}");
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            var result = await _departmentService.UpdateDepartmentAsync(departmentId, department);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteDepartment(short departmentId)
        {
            _logger.LogInformation($"Deleting Department with department id: {departmentId}");
            var result = await _departmentService.DeleteDepartmentAsync(departmentId);
            if (result != null)
                return NoContent();
            return BadRequest();
        }
    }
}