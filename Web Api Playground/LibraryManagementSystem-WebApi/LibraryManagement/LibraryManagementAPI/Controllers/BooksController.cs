using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] BookVm bookVm)
        {
            _logger.LogInformation("Adding a Book");
            var book = _mapper.Map<BookVm, Book>(bookVm);
            return Ok(await _bookRepository.AddBookAsync(book));
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            _logger.LogInformation("Getting Available Books Details");
            return Ok(await _bookRepository.GetBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(int bookId)
        {
            _logger.LogInformation($"Getting Available Book Detail by Book Id: {bookId}");
            var result = _bookRepository.GetBookById(bookId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookDetails([FromBody] BookVm bookVm, int bookId)
        {
            var book = _mapper.Map<BookVm, Book>(bookVm);
            _logger.LogInformation($"Updating a Book details with bookId: {bookId}");
            var result = _bookRepository.UpdateBookAsync(book, bookId);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            _logger.LogInformation($"Deleting a Book with {bookId}");
            var result = _bookRepository.DeleteBookAsync(bookId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}