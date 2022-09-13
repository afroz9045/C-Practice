using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class BooksController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository bookRepository, IBookService bookService, IMapper mapper, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{bookName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookByName(string bookName)
        {
            _logger.LogInformation($"Getting Book by book name {bookName}");
            _logger.LogInformation($"Getting Book by book name {bookName}");
            var result = await _bookService.GetBookByNameAsync(bookName);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}