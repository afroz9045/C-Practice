using AutoMapper;
using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Core.Infrastructure.Repositories;
using EmployeeRecordBook.Infrastructure.Data;
using EmployeeRecordBook.Infrastructure.Repositories.Dapper;
using EmployeeRecordBook.Infrastructure.Repositories.EntityFramework;
using EmployeeRecordBook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecordBook.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeRepository.GetEmployeesAsync();
            if (result.Count() <= 0)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("MinimumEmpDetails")]
        public async Task<IEnumerable<EmployeeMinimumData>> GetMinimumEmployeeData()
        {
            return await _employeeRepository.GetEmployeesByView();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<EmployeeMinimumData>>> Get(int id)
        {
            if (id <= 0 || id >= 9999999)
                return BadRequest("Invalid Id");
            var result = await _employeeRepository.GetEmployeeAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<Employee> Post([FromBody] EmployeeVm employeeVm)
        {
            //var employee = new Employee { Name = employeeVm.Name, Email = employeeVm.Email, Salary = employeeVm.Salary, DepartmentId = employeeVm.DepartmentId };
            var employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
            return await _employeeRepository.CreateAsync(employee);
        }

        [HttpPut("{id}")]
        public async Task<Employee> Put(int id, [FromBody] EmployeeVm employeeVm)
        {
            var employee = new Employee { Id = employeeVm.Id ?? 0, Name = employeeVm.Name, Email = employeeVm.Email, Salary = employeeVm.Salary, DepartmentId = employeeVm.DepartmentId };
            return await _employeeRepository.UpdateAsync(id, employee);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
