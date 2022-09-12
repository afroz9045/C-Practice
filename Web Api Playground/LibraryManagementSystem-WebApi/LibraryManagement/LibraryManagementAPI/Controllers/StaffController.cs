using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class StaffController : ApiController
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffService staffService, IMapper mapper, ILogger<StaffController> logger)
        {
            _staffService = staffService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddStaff([FromBody] StaffVm staffVm)
        {
            _logger.LogInformation($"Adding Staff details");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var result = await _staffService.AddStaffAsync(staff);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaff()
        {
            _logger.LogInformation("Getting staff details");
            var staffResult = await _staffService.GetStaffAsync();
            if (staffResult != null)
                return Ok(staffResult);
            return NotFound();
        }

        [HttpGet("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffById(string staffId)
        {
            _logger.LogInformation($"Getting staff details with staff id {staffId}");
            var result = Ok(await _staffService.GetStaffByIdAsync(staffId));
            if (result != null)
                return result;
            return NotFound();
        }

        [HttpPut("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffVm staffVm, string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var result = _staffService.UpdateStaffAsync(staff, staffId);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{staffId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteStaff(string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var result = _staffService.DeleteStaffAsync(staffId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}