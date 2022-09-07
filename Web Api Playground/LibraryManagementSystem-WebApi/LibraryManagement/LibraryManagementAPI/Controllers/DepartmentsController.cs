using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
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
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok(await _departmentRepository.AddDepartmentAsync(department));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentVm departmentVm, short id)
        {
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
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