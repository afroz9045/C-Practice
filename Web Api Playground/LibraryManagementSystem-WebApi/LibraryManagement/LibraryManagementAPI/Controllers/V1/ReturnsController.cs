using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ReturnsController : ApiController
    {
        private readonly IReturnService _returnService;
        private readonly IIssueRepository _issueRepository;
        private readonly IPenaltyService _penaltyService;
        private readonly IBookRepository _bookRepository;
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IReturnRepository _returnRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReturnsController> _logger;

        public ReturnsController(IReturnService returnService, IIssueRepository issueRepository, IPenaltyService penaltyService, IBookRepository bookRepository, IPenaltyRepository penaltyRepository, IReturnRepository returnRepository, IMapper mapper, ILogger<ReturnsController> logger)
        {
            _returnService = returnService;
            _issueRepository = issueRepository;
            _penaltyService = penaltyService;
            _bookRepository = bookRepository;
            _penaltyRepository = penaltyRepository;
            _returnRepository = returnRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("{issueId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddReturn([FromBody] ReturnVm returnVm, [Required] short issueId)
        {
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
            if (issueDetails == null)
            {
                return BadRequest($"No book issued to this issued Id : {issueId}");
            }
            var bookDetails = await _bookRepository.GetBookById(issueDetails.BookId);
            var penaltyData = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            Penalty? isPenalty = _penaltyService.IsPenalty(issueId, penaltyData, issueDetails);
            var isPenaltyExist = isPenalty != null ? await _penaltyRepository.IsPenalty(isPenalty) : null;
            if (isPenaltyExist == null || isPenaltyExist.PenaltyPaidStatus == true)
            {
                var returnResult = _returnService.AddReturn(returnBook, issueId, isPenaltyExist, bookDetails, issueDetails);
                if (returnResult.Item1 != null && returnResult.Item2 != null)
                {
                    _logger.LogInformation($"Adding Book return with issue id : {issueId}");
                    var returnRecordResult = await _returnRepository.AddReturnAsync(returnResult.Item1, returnResult.Item2, issueDetails);
                    return Ok(returnRecordResult);
                }
            }
            return NotFound("Please Check with your Issued Books and Penalty!");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookReturn()
        {
            _logger.LogInformation($"Getting Books returns");
            var bookReturnResult = await _returnRepository.GetReturnAsync();
            if (bookReturnResult != null)
                return Ok(bookReturnResult);
            return NotFound();
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

        [HttpPut("{bookReturnId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateReturnBookDetails(int bookReturnId, [FromBody] ReturnVm returnVm)
        {
            _logger.LogInformation($"Updating book return details with book return id {bookReturnId}");
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            var existingReturnRecord = await _returnRepository.GetReturnByIdAsync(bookReturnId);
            var returnToBeUpdate = _returnService.UpdateReturnAsync(bookReturnId, existingReturnRecord, returnBook);
            if (existingReturnRecord != null && returnToBeUpdate != null)
            {
                var updatedReturnDetails = await _returnRepository.UpdateReturnAsync(returnToBeUpdate);
                return Ok(updatedReturnDetails);
            }
            return BadRequest();
        }

        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteReturnBook(int returnId)
        {
            _logger.LogInformation($"Deleting Book Return details with return id : {returnId}");
            var bookReturnToBeDelete = await _returnRepository.GetReturnByIdAsync(returnId);
            if (bookReturnToBeDelete != null)
            {
                var returnDelete = await _returnRepository.DeleteReturnAsync(bookReturnToBeDelete);
                return Ok(returnDelete);
            }
            return BadRequest("Book Return not found");
        }
    }
}