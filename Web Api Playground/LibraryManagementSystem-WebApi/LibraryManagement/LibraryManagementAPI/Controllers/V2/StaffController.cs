using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
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
    }
}