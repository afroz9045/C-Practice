using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class StaffController : ApiController
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffRepository staffRepository, IMapper mapper, ILogger<StaffController> logger)
        {
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
            return Ok(await _staffRepository.AddStaffAsync(staff));
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaff()
        {
            _logger.LogInformation("Getting staff details");
            return Ok(await _staffRepository.GetStaffAsync());
        }

        [HttpGet("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffById(string staffId)
        {
            _logger.LogInformation($"Getting staff details with staff id {staffId}");
            var result = Ok(await _staffRepository.GetStaffByIDAsync(staffId));
            return result;
        }

        [HttpPut("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffVm staffVm, string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);
            var result = _staffRepository.UpdateStaffAsync(staff, staffId);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{staffId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteStaff(string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var result = _staffRepository.DeleteStaffAsync(staffId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}