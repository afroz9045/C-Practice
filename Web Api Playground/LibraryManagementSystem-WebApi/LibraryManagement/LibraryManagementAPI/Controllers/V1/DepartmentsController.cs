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
    [Route("v{version:apiVersion}/departments")]
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
        [Authorize(Roles = "Director,Principle")]
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
            var departmentDto = addedDepartment != null ? _mapper.Map<Department, DepartmentDto>(addedDepartment) : null;
            if (departmentDto != null)
                return Ok(departmentDto);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartments()
        {
            _logger.LogInformation("Getting Departments details");
            var departments = await _departmentRepository.GetDepartmentsAsync();
            var departmentDto = departments != null ? _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departments) : null;
            if (departmentDto != null)
                return Ok(departmentDto);
            return NotFound();
        }

        [HttpGet("{departmentId:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentById(short departmentId)
        {
            _logger.LogInformation($"Getting Department details by id: {departmentId}");
            var departmentResult = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            var departmentDto = departmentResult != null ? _mapper.Map<Department, DepartmentDto>(departmentResult) : null;
            if (departmentDto != null)
                return Ok(departmentDto);
            return BadRequest();
        }

        [HttpGet("{departmentName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentByName(string departmentName)
        {
            _logger.LogInformation($"Getting Department details by department name: {departmentName}");
            var departmentResult = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            var departmentDto = departmentResult != null ? _mapper.Map<Department, DepartmentDto>(departmentResult) : null;
            if (departmentDto != null)
                return Ok(departmentDto);
            return BadRequest();
        }

        [HttpPut("{deptId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Director,Principle")]
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
            var departmentDto = updatedDepartment != null ? _mapper.Map<Department, DepartmentDto>(updatedDepartment) : null;
            if (departmentDto != null)
                return Ok(departmentDto);
            return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        [Authorize(Roles = "Director,Principle")]
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