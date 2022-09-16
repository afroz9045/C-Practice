using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [HttpGet("{designationName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetDesignationByName(string designationName)
        {
            _logger.LogInformation($"Getting designation by designation name: {designationName}");
            var result = await _designationRepository.GetDesignationByNameAsync(designationName);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}