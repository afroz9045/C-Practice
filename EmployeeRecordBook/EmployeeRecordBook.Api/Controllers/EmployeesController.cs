using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Core.Infrastructure.Repositories;
using EmployeeRecordBook.Infrastructure.Data;
using EmployeeRecordBook.Infrastructure.Repositories.EntityFramework;
using EmployeeRecordBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRecordBook.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController()
        {
            _employeeRepository = new EmployeeRepository(new EmployeeContext());
        }


        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await _employeeRepository.GetEmployeeAsync(id);
        }


        [HttpPost]
        public async Task<Employee> Post([FromBody] EmployeeVm employeeVm)
        {
            var employee = new Employee { Name = employeeVm.Name, Email = employeeVm.Email, Salary = employeeVm.Salary, DepartmentId = employeeVm.DepartmentId };
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
