using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    public class DesignationsController : CommonController
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IMapper _mapper;

        public DesignationsController(IDesignationRepository designationRepository, IMapper mapper)
        {
            _designationRepository = designationRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationVm designationVm)
        {
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            return Ok(await _designationRepository.AddDesignationAsync(designation));
        }

        [HttpGet]
        public async Task<ActionResult> GetDesignations()
        {
            return Ok(await _designationRepository.GetDesignationAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDesignationById(Guid id)
        {
            var result = Ok(await _designationRepository.GetDesignationByIdAsync(id));
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDesignation([FromBody] DesignationVm designationVm, Guid id)
        {
            var designation = _mapper.Map<DesignationVm, Designation>(designationVm);
            var result = _designationRepository.UpdateDesignationAsync(id, designation);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDesignation(Guid id)
        {
            var designation = _designationRepository.DeleteDepartmentAsync(id);
            if (designation != null)
                return Ok(await designation);
            return NotFound();
        }
    }
}