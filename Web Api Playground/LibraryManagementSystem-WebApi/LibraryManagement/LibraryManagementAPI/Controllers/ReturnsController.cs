using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class ReturnsController : ApiController
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReturnsController> _logger;

        public ReturnsController(IReturnRepository returnRepository, IMapper mapper, ILogger<ReturnsController> logger)
        {
            _returnRepository = returnRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("{issueId}")]
        public async Task<ActionResult> AddReturn([FromBody] ReturnVm returnVm, short issueId)
        {
            _logger.LogInformation($"Adding Book return with issue id : {issueId}");
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            var returnResult = await _returnRepository.AddReturnAsync(returnBook, issueId);
            if (returnResult != null && returnResult.ReturnId > 0)
            {
                return Ok(returnResult);
            }
            return NotFound("Please Check with your Issued Books and Penalty!");
        }

        [HttpGet]
        public async Task<ActionResult> GetBookReturn()
        {
            _logger.LogInformation($"Getting Books returns");
            return Ok(await _returnRepository.GetReturnAsync());
        }

        [HttpGet("{bookReturnId}")]
        public async Task<ActionResult> GetBookReturnById(int bookReturnId)
        {
            _logger.LogInformation($"Getting Book return by return id {bookReturnId}");
            return Ok(await _returnRepository.GetReturnByIdAsync(bookReturnId));
        }

        [HttpPut("{bookReturnId}")]
        public async Task<ActionResult> UpdateReturnBookDetails(int bookReturnId, [FromBody] ReturnVm returnVm)
        {
            _logger.LogInformation($"Updating book return details with book return id {bookReturnId}");
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            var result = Ok(await _returnRepository.UpdateReturnAsync(bookReturnId, returnBook));
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteReturnBook(int returnId)
        {
            _logger.LogInformation($"Deleting Book Return details with return id : {returnId}");
            var returnDelete = await _returnRepository.DeleteReturnAsync(returnId);
            if (returnDelete != null)
                return Ok(returnDelete);
            return NotFound();
        }
    }
}