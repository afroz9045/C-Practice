using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Api.Controllers
{
    public class DesignationsController : ApiController
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DesignationsController> _logger;

        public DesignationsController(IDesignationRepository designationRepository, IMapper mapper, ILogger<DesignationsController> logger)
        {
            _designationRepository = designationRepository;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationVm designationVm)
        {
            _logger.LogInformation("Adding designation");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            return Ok(await _designationRepository.AddDesignationAsync(designation));
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignations()
        {
            _logger.LogInformation("Getting designations details");
            return Ok(await _designationRepository.GetDesignationAsync());
        }

        [HttpGet("{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignationById(string designationId)
        {
            _logger.LogInformation($"Getting designation by designation id: {designationId}");
            var result = Ok(await _designationRepository.GetDesignationByIdAsync(designationId));
            return result;
        }

        [HttpPut("{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateDesignation([FromBody] DesignationVm designationVm, string designationId)
        {
            _logger.LogInformation($"Update designation details by designation id: {designationId}");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var result = await _designationRepository.UpdateDesignationAsync(designationId, designation);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{designationId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteDesignation(string designationId)
        {
            _logger.LogInformation($"Deleting designation details by designation id: {designationId}");
            var designation = await _designationRepository.DeleteDesignationAsync(designationId);
            if (designation != null)
                return Ok(designation);
            return NotFound();
        }
    }
}