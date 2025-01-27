﻿using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/staffs")]
    public class StaffController : ApiController
    {
        private readonly IStaffService _staffService;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;
        private readonly IConfiguration _configuration;

        public StaffController(IStaffService staffService, IStaffRepository staffRepository, IMapper mapper, ILogger<StaffController> logger, IConfiguration configuration)
        {
            _staffService = staffService;
            _staffRepository = staffRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
       // [Authorize(Roles = "Director,Principle")]
        public async Task<ActionResult> AddStaff([FromBody] StaffVm staffVm)
        {
            _logger.LogInformation($"Adding Staff details");
            var staff = _mapper.Map<StaffVm, Staff>(staffVm);

            var recentStaff = await _staffRepository.GetRecentInsertedStaff();
            var staffToBeAdd = _staffService.AddStaff(staff, recentStaff);
            var addedStaff = staffToBeAdd != null ? await _staffRepository.AddStaffAsync(staffToBeAdd) : null;
            if (addedStaff != null)
            {
                using (var client = new HttpClient())
                {
                    var staffWithCredentials = new RegistrationVm()
                    {
                        Email = staffVm.Email,
                        FullName = staffVm.StaffName,
                        Password = staffVm.Password,
                        StaffId = addedStaff.StaffId
                    };
                    client.BaseAddress = new Uri(_configuration.GetSection("Constants").GetSection("AuthenticationBaseUrl").Value);
                    var content = new StringContent(JsonSerializer.Serialize(staffWithCredentials), System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(_configuration.GetSection("Constants").GetSection("AuthenticationSubUrl").Value, content);
                }
                var staffDto = _mapper.Map<Staff, StaffDto>(addedStaff);
                return Ok(staffDto);
            }
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaff()
        {
            _logger.LogInformation("Getting staff details");
            var staffResult = await _staffRepository.GetStaffAsync();
            if (staffResult != null)
            {
                var staffsDto = _mapper.Map<IEnumerable<Staff>, IEnumerable<StaffDto>>(staffResult);
                return Ok(staffsDto);
            }
            return NotFound();
        }

        [HttpGet("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffById(string staffId)
        {
            _logger.LogInformation($"Getting staff details with staff id {staffId}");
            var staffResult = await _staffRepository.GetStaffByIdAsync(staffId);
            if (staffResult != null)
            {
                var staffDto = _mapper.Map<Staff, StaffDto>(staffResult);
                return Ok(staffDto);
            }
            return NotFound();
        }

        [HttpGet("name/{staffName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetStaffByName(string staffName)
        {
            _logger.LogInformation($"Getting staff details with staff name {staffName}");
            var staffResult = await _staffRepository.GetStaffByName(staffName);
            if (staffResult != null)
            {
                var staffDto = _mapper.Map<Staff, StaffDto>(staffResult);
                return Ok(staffDto);
            }
            return NotFound();
        }

        [HttpPut("{staffId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Director,Principle")]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffUpdateVm staffUpdateVm, string staffId)
        {
            _logger.LogInformation($"Updating staff details with staff id {staffId}");
            var staff = _mapper.Map<StaffUpdateVm, Staff>(staffUpdateVm);
            var existingStaffRecord = await _staffRepository.GetStaffByIdAsync(staffId);
            if (existingStaffRecord != null)
            {
                var staffToBeUpdate = _staffService.UpdateStaff(existingStaffRecord, staff);
                var updatedStaff = await _staffRepository.UpdateStaffAsync(staffToBeUpdate);
                var staffDto = _mapper.Map<Staff, StaffDto>(updatedStaff!);
                return Ok(staffDto);
            }
            return BadRequest();
        }

        [HttpDelete("{staffId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        [Authorize(Roles = "Director,Principle")]
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
                return NoContent();
            return NotFound();
        }
    }
}