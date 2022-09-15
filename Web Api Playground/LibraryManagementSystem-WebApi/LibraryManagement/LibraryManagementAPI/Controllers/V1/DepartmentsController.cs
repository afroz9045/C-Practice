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
            var isDepartmentAlreadyAvailable = await _departmentRepository.GetDepartmentByNameAsync(department.DepartmentName);
            if (isDepartmentAlreadyAvailable != null)
            {
                return BadRequest("Department already exist");
            }
            var departmentToBeAdd = _departmentService.AddDepartmentAsync(department);
            var addedDepartment = await _departmentRepository.AddDepartmentAsync(departmentToBeAdd!);
            if (addedDepartment != null)
                return Ok(addedDepartment);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartments()
        {
            _logger.LogInformation("Getting Departments details");
            var departments = await _departmentRepository.GetDepartmentsAsync();
            if (departments != null)
                return Ok(departments);
            return NotFound();
        }

        [HttpGet("{departmentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentById(short departmentId)
        {
            _logger.LogInformation($"Getting Department details by id: {departmentId}");
            var departmentResult = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            if (departmentResult != null)
                return Ok(departmentResult);
            return BadRequest();
        }

        [HttpGet("{departmentName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentByName(string departmentName)
        {
            _logger.LogInformation($"Getting Department details by department name: {departmentName}");
            var departmentResult = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            if (departmentResult != null)
                return Ok(departmentResult);
            return BadRequest();
        }

        [HttpPut("{deptId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentVm departmentVm, short deptId)
        {
            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(deptId);
            if (existingDepartment == null)
            {
                return BadRequest("Department not found!");
            }
            _logger.LogInformation($"Updating Department with department id: {deptId}");
            var departmentToBeUpdate = _mapper.Map<DepartmentVm, Department>(departmentVm);
            var result = _departmentService.UpdateDepartmentAsync(existingDepartment, departmentToBeUpdate);
            var updatedDepartment = await _departmentRepository.UpdateDepartmentAsync(result!);
            if (updatedDepartment != null)
                return Ok(updatedDepartment);
            return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteDepartment(short departmentId)
        {
            var departmentToBeDelete = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            if (departmentToBeDelete == null)
            {
                return BadRequest("Department not found");
            }
            _logger.LogInformation($"Deleting Department with department id: {departmentId}");
            var result = await _departmentRepository.DeleteDepartmentAsync(departmentToBeDelete);
            if (result != null)
                return NoContent();
            return BadRequest();
        }
    }
}