using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    public class DesignationsController : ApiController
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IDesignationService _designationService;
        private readonly IMapper _mapper;
        private readonly ILogger<DesignationsController> _logger;

        public DesignationsController(IDesignationRepository designationRepository, IDesignationService designationService, IMapper mapper, ILogger<DesignationsController> logger)
        {
            _designationRepository = designationRepository;
            _designationService = designationService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationVm designationVm)
        {
            _logger.LogInformation("Adding designation");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var designationAdded = await _designationService.AddDesignationAsync(designation);
            if (designationAdded != null)
                return Ok(designationAdded);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignations()
        {
            _logger.LogInformation("Getting designations details");
            var designations = await _designationService.GetDesignationAsync();
            if (designations != null)
                return Ok(designations);
            return NotFound();
        }

        [HttpGet("{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignationById(string designationId)
        {
            _logger.LogInformation($"Getting designation by designation id: {designationId}");
            var result = await _designationService.GetDesignationByIdAsync(designationId);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("{designationName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignationByName(string designationName)
        {
            _logger.LogInformation($"Getting designation by designation designation name: {designationName}");
            var result = await _designationService.GetDesignationByNameAsync(designationName);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateDesignation([FromBody] DesignationVm designationVm, string designationId)
        {
            _logger.LogInformation($"Update designation details by designation id: {designationId}");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var result = await _designationService.UpdateDesignationAsync(designationId, designation);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{designationId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteDesignation(string designationId)
        {
            _logger.LogInformation($"Deleting designation details by designation id: {designationId}");
            var designation = await _designationService.DeleteDesignationAsync(designationId);
            if (designation != null)
                return NoContent();
            return BadRequest();
        }
    }
}