using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class ReturnsController : ApiController
    {
        private readonly IReturnService _returnService;
        private readonly IReturnRepository _returnRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReturnsController> _logger;

        public ReturnsController(IReturnService returnService, IReturnRepository returnRepository, IMapper mapper, ILogger<ReturnsController> logger)
        {
            _returnService = returnService;
            _returnRepository = returnRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{bookReturnId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookReturnById(int bookReturnId)
        {
            _logger.LogInformation($"Getting Book return by return id {bookReturnId}");
            var bookReturnResult = await _returnRepository.GetReturnByIdAsync(bookReturnId);
            if (bookReturnResult != null)
                return Ok(bookReturnResult);
            return NotFound();
        }
    }
}