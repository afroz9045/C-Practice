using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Director,Principle")]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationVm designationVm)
        {
            var designationRecord = await _designationRepository.GetDesignationByNameAsync(designationVm.DesignationName);
            if (designationRecord != null)
            {
                return BadRequest("Designation already available");
            }
            _logger.LogInformation("Adding designation");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var designationToBeAdd = await _designationService.AddDesignationAsync(designation, designationRecord);
            var addedDesignation = await _designationRepository.AddDesignationAsync(designationToBeAdd!);
            var designationDto = addedDesignation != null ? _mapper.Map<Designation, DesignationDto>(addedDesignation) : null;
            if (designationDto != null)
                return Ok(designationDto);
            return BadRequest();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle,Professor,Lecturer,Associate Lecturer,HOD,Accountant,Clerk")]
        public async Task<ActionResult> GetDesignations()
        {
            _logger.LogInformation("Getting designations details");
            var designations = await _designationRepository.GetDesignationAsync();
            var designationDto = designations != null ? _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(designations) : null;
            if (designationDto != null)
                return Ok(designationDto);
            return NotFound();
        }

        [HttpGet("id/{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle,Professor,Lecturer,Associate Lecturer,HOD,Accountant,Clerk")]
        public async Task<ActionResult> GetDesignationById(string designationId)
        {
            _logger.LogInformation($"Getting designation by designation id: {designationId}");
            var designationResult = await _designationRepository.GetDesignationByIdAsync(designationId);
            var designationDto = designationResult != null ? _mapper.Map<Designation, DesignationDto>(designationResult) : null;
            if (designationDto != null)
                return Ok(designationDto);
            return NotFound();
        }

        [HttpGet("{designationName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle,Professor,Lecturer,Associate Lecturer,HOD,Accountant,Clerk")]
        public async Task<ActionResult> GetDesignationByName(string designationName)
        {
            _logger.LogInformation($"Getting designation by designation name: {designationName}");
            var designationResult = await _designationRepository.GetDesignationByNameAsync(designationName);
            var designationDto = designationResult != null ? _mapper.Map<Designation, DesignationDto>(designationResult) : null;
            if (designationDto != null)
                return Ok(designationDto);
            return NotFound();
        }

        [HttpPut("{designationId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Director,Principle")]
        public async Task<ActionResult> UpdateDesignation([FromBody] DesignationVm designationVm, string designationId)
        {
            var designationRecord = await _designationRepository.GetDesignationByIdAsync(designationId);
            if (designationRecord == null)
            {
                return BadRequest("Designation is not exist!");
            }
            _logger.LogInformation($"Update designation details by designation id: {designationId}");
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var designationToBeUpdate = _designationService.UpdateDesignationAsync(designationId, designation, designationRecord);
            var updatedDesignation = await _designationRepository.UpdateDesignationAsync(designationToBeUpdate!);
            var designationDto = updatedDesignation != null ? _mapper.Map<Designation, DesignationDto>(updatedDesignation) : null;
            if (designationDto != null)
                return Ok(designationDto);
            return BadRequest();
        }

        [HttpDelete("{designationId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        [Authorize(Roles = "Director,Principle")]
        public async Task<ActionResult> DeleteDesignation(string designationId)
        {
            var designationRecord = await _designationRepository.GetDesignationByIdAsync(designationId);
            if (designationRecord == null)
            {
                return BadRequest("Designation is not exist");
            }
            _logger.LogInformation($"Deleting designation details by designation id: {designationId}");
            var deletedDesignation = await _designationRepository.DeleteDesignationAsync(designationRecord);
            if (deletedDesignation != null)
                return NoContent();
            return BadRequest();
        }
    }
}