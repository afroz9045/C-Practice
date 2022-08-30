using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            return Ok(await _departmentRepository.GetDepartmentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartmentById(short id)
        {
            var result = Ok(await _departmentRepository.GetDepartmentByIdAsync(id));
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentVm departmentVm)
        {
            var department = new Department
            {
                DepartmentName = departmentVm.DepartmentName
            };
            return Ok(await _departmentRepository.AddDepartmentAsync(department));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentDto department, short id)
        {
            var result = _departmentRepository.UpdateDepartmentAsync(id, department);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(short id)
        {
            var result = _departmentRepository.DeleteDepartmentAsync(id);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}