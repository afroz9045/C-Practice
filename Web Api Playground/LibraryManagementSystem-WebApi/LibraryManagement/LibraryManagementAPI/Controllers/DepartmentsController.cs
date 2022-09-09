using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentVm departmentVm)
        {
            _logger.LogInformation("Adding Department");
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok(await _departmentRepository.AddDepartmentAsync(department));
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartments()
        {
            _logger.LogInformation("Getting Departments details");
            return Ok(await _departmentRepository.GetDepartmentsAsync());
        }

        [HttpGet("{departmentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDepartmentById(short departmentId)
        {
            _logger.LogInformation($"Getting Department details by id: {departmentId}");
            var result = Ok(await _departmentRepository.GetDepartmentByIdAsync(departmentId));
            return result;
        }

        [HttpPut("{departmentId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentVm departmentVm, short departmentId)
        {
            _logger.LogInformation($"Updating Department with department id: {departmentId}");
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            var result = _departmentRepository.UpdateDepartmentAsync(departmentId, department);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteDepartment(short departmentId)
        {
            _logger.LogInformation($"Deleting Department with department id: {departmentId}");
            var result = _departmentRepository.DeleteDepartmentAsync(departmentId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}