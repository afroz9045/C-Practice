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
    [ApiConventionType(typeof(DefaultApiConventions))]
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

        /// <summary>
        /// This method can be use to add new books
        /// </summary>
        /// <param name="bookVm">book</param>
        /// <returns>it returns response code 201 or else it return response code 400</returns>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddBook([FromBody] BookVm bookVm)
        {
            _logger.LogInformation("Adding a Book");
            var book = _mapper.Map<BookVm, Book>(bookVm);
            var bookAddedResult = await _bookService.AddBookAsync(book);
            if (bookAddedResult != null)
                return Ok(bookAddedResult);
            return BadRequest("Book is not added,check details and try again");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBooks()
        {
            _logger.LogInformation("Getting Available Books Details");
            var booksResult = await _bookService.GetBooksAsync();
            if (booksResult != null)
                return Ok(booksResult);
            return NotFound("Books not found");
        }

        [HttpGet("/bookid/{bookId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookById(int bookId)
        {
            if (bookId == 0)
            {
                return BadRequest($"Invalid book id {bookId}");
            }
            _logger.LogInformation($"Getting Available Book Detail by Book Id: {bookId}");
            var result = _bookService.GetBookByBookId(bookId);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }

        [HttpGet("/bookname/{bookName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookByName(string bookName)
        {
            _logger.LogInformation($"Getting Book by book name {bookName}");
            var result = await _bookService.GetBookByNameAsync(bookName);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("{bookId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateBookDetails([FromBody] BookVm bookVm, int bookId)
        {
            var book = _mapper.Map<BookVm, Book>(bookVm);
            _logger.LogInformation($"Updating a Book details with bookId: {bookId}");
            var result = await _bookService.UpdateBooksAsync(book, bookId);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{bookId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            _logger.LogInformation($"Deleting a Book with {bookId}");
            var result = await _bookService.DeleteBookAsync(bookId);
            if (result != null)
                return NoContent();
            return BadRequest();
        }
    }
}