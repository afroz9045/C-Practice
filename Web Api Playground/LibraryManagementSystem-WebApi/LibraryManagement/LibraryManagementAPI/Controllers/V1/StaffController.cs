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
    public class StaffController : ApiController
    {
        private readonly IStaffService _staffService;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffService staffService, IStaffRepository staffRepository, IMapper mapper, ILogger<StaffController> logger)
        {
            _staffService = staffService;
            _staffRepository = staffRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddStaff([FromBody] StaffVm staffVm)
        {
            _logger.LogInformation($"Adding Staff details");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var staffToBeAdd = await _staffService.AddStaffAsync(staff);
            var addedStaff = staffToBeAdd != null ? await _staffRepository.AddStaffAsync(staffToBeAdd) : null;
            if (addedStaff != null)
                return Ok(addedStaff);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaff()
        {
            _logger.LogInformation("Getting staff details");
            var staffResult = await _staffRepository.GetStaffAsync();
            if (staffResult != null)
                return Ok(staffResult);
            return NotFound();
        }

        [HttpGet("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffById(string staffId)
        {
            _logger.LogInformation($"Getting staff details with staff id {staffId}");
            var result = await _staffRepository.GetStaffByIdAsync(staffId);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("{staffName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffByName(string staffName)
        {
            _logger.LogInformation($"Getting staff details with staff name {staffName}");
            var result = await _staffRepository.GetStaffByName(staffName);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffVm staffVm, string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var existingStaffRecord = await _staffRepository.GetStaffByIdAsync(staffId);
            if (existingStaffRecord != null)
            {
                var staffToBeUpdate = _staffService.UpdateStaffAsync(existingStaffRecord, staff);
                var updatedStaff = await _staffRepository.UpdateStaffAsync(staffToBeUpdate);
                return Ok(updatedStaff);
            }
            return BadRequest();
        }

        [HttpDelete("{staffId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteStaff(string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var staffToBeDelete = await _staffRepository.GetStaffByIdAsync(staffId);
            if (staffToBeDelete == null)
            {
                return BadRequest("Staff not found");
            }
            var deletedStaff = await _staffRepository.DeleteStaffAsync(staffToBeDelete);
            if (deletedStaff != null)
                return Ok(deletedStaff);
            return NotFound();
        }
    }
}