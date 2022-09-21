using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// This method can be use to add new books
        /// </summary>
        /// <param name="bookVm">book</param>
        /// <returns>it returns response code 201 or else it return response code 400</returns>
        ///
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddBook([FromBody] BookVm bookVm)
        {
            _logger.LogInformation("Adding a Book");
            var book = _mapper.Map<BookVm, Book>(bookVm);
            var existingBook = await _bookRepository.GetBookByBookName(bookVm.BookName);
            var bookResult = _bookService.AddBookAsync(book, existingBook);
            if (bookResult != null && bookResult.StockAvailable == 1)
            {
                var bookAddedResult = await _bookRepository.AddBookAsync(bookResult);
                return Ok(bookAddedResult);
            }
            else if (bookResult != null && bookResult.StockAvailable > 1)
            {
                var bookAddedResult = await _bookRepository.AddBookAsync(bookResult);
                return Ok($"Book stock updated with book id: {bookResult.BookId}");
            }
            return BadRequest("Book is not added,check details and try again");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBooks()
        {
            _logger.LogInformation("Getting Available Books Details");
            var booksFromRepo = await _bookRepository.GetBooksAsync();
            if (booksFromRepo != null)
                return Ok(booksFromRepo);
            return NotFound("Books not found");
        }

        [HttpGet("{bookId:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                //return BadRequest();
                throw new ArgumentException();
            }
            _logger.LogInformation($"Getting Available Book Detail by Book Id: {bookId}");
            var bookByBookId = await _bookRepository.GetBookById(bookId);
            if (bookByBookId != null)
                return Ok(bookByBookId);
            return NotFound();
        }

        [HttpGet("{bookName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookByName(string bookName)
        {
            _logger.LogInformation($"Getting Book by book name {bookName}");
            var bookByBookName = await _bookRepository.GetBookByBookName(bookName);
            if (bookByBookName != null)
                return Ok(bookByBookName);
            return NotFound();
        }

        [HttpPut("{bookId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateBookDetails([FromBody] BookVm bookVm, int bookId)
        {
            var book = _mapper.Map<BookVm, Book>(bookVm);
            _logger.LogInformation($"Updating a Book details with bookId: {bookId}");
            var existingBook = await _bookRepository.GetBookById(bookId);
            if (existingBook == null)
            {
                return BadRequest("Book is not exist!");
            }
            var result = _bookService.UpdateBooksAsync(book, existingBook);
            var updatedBookDetails = await _bookRepository.UpdateBookAsync(result!);
            if (updatedBookDetails != null)
                return Ok(updatedBookDetails);
            return BadRequest();
        }

        [HttpDelete("{bookId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            _logger.LogInformation($"Deleting a Book with {bookId}");
            var existingBook = await _bookRepository.GetBookById(bookId);
            if (existingBook == null)
            {
                return BadRequest("Book is not exist!");
            }
            var result = await _bookRepository.DeleteBookAsync(existingBook!);
            if (result != null)
                return NoContent();
            return BadRequest();
        }
    }
}