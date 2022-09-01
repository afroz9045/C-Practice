using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class StaffController : CommonController
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public StaffController(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddStaff([FromBody] StaffVm staffVm)
        {
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            return Ok(await _staffRepository.AddStaffAsync(staff));
        }

        [HttpGet]
        public async Task<ActionResult> GetStaff()
        {
            return Ok(await _staffRepository.GetStaffAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStaffById(Guid id)
        {
            var result = Ok(await _staffRepository.GetStaffByIDAsync(id));
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffVm staffVm, Guid id)
        {
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var result = _staffRepository.UpdateStaffAsync(staff, id);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            var result = _staffRepository.DeleteStaffAsync(id);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}