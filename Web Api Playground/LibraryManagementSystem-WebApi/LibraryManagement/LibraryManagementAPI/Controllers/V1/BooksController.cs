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

namespace LibraryManagement.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IReturnRepository _returnRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, IReturnRepository returnRepository, IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _returnRepository = returnRepository;
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
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult> AddBook([FromBody] BookVm bookVm)
        {
            _logger.LogInformation("Adding a Book");
            var book = _mapper.Map<BookVm, Book>(bookVm);
            var existingBook = await _bookRepository.GetBookByBookName(bookVm.BookName);
            var bookResult = _bookService.AddBookAsync(book, existingBook);
            if (bookResult != null && bookResult.StockAvailable == 1)
            {
                var bookAddedResult = await _bookRepository.AddBookAsync(bookResult);
                var bookDto = bookAddedResult != null ? _mapper.Map<Book, BookDto>(bookAddedResult) : null;
                return Ok(bookDto);
            }
            else if (bookResult != null && bookResult.StockAvailable > 1)
            {
                var bookAddedResult = await _bookRepository.AddBookAsync(bookResult);
                var bookDto = bookAddedResult != null ? _mapper.Map<Book, BookDto>(bookAddedResult) : null;
                if (bookDto != null)
                {
                    bookDto.StockUpdate = true;
                    return Ok(bookDto);
                }
            }
            return BadRequest("Book is not added,check details and try again");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBooks()
        {
            _logger.LogInformation("Getting Available Books Details");
            var booksFromRepo = await _bookRepository.GetBooksAsync();
            var booksDto = booksFromRepo != null ? _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(booksFromRepo) : null;
            if (booksDto != null)
                return Ok(booksDto);
            return NotFound("Books not found");
        }

        [HttpGet("{bookId:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Getting Available Book Detail by Book Id: {bookId}");
            var bookByBookId = await _bookRepository.GetBookById(bookId);
            var bookDto = bookByBookId != null ? _mapper.Map<Book, BookDto>(bookByBookId) : null;
            if (bookDto != null)
                return Ok(bookDto);
            return NotFound();
        }

        [HttpGet("{bookName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetBookByName(string bookName)
        {
            _logger.LogInformation($"Getting Book by book name {bookName}");
            var bookByBookName = await _bookRepository.GetBookByBookName(bookName);
            var bookDto = bookByBookName != null ? _mapper.Map<Book, BookDto>(bookByBookName) : null;
            if (bookDto != null)
                return Ok(bookDto);
            return NotFound();
        }

        [HttpGet("outofstock")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Authorize(Roles = "Librarian,Director,Principle")]
        public async Task<ActionResult> GetOutOfStockBooks()
        {
            var outOfStockBooks = await _bookRepository.GetOutOfStockBooks();
            var bookDto = outOfStockBooks != null ? _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(outOfStockBooks) : null;
            if (bookDto != null)
                return Ok(bookDto);
            return BadRequest("No books are out of stock");
        }

        [HttpPut("stockupdate")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult> UpdateBookStock([FromBody] BookStockUpdateVm bookStockUpdate)
        {
            var availableBook = await _bookRepository.GetBookById(bookStockUpdate.BookId);
            if (availableBook == null)
            {
                return BadRequest("Entered Book id is not found!");
            }
            else if (bookStockUpdate.StockToBeUpdate == availableBook.StockAvailable)
            {
                return BadRequest("Already present stock can't be update again!");
            }
            else if (availableBook != null)
            {
                var stockToBeUpdate = _bookService.UpdateBookStock(availableBook, bookStockUpdate.StockToBeUpdate);
                var updatedStock = await _bookRepository.UpdateBookAsync(stockToBeUpdate);

                return Ok(updatedStock);
            }
            return BadRequest();
        }

        [HttpPut("{bookId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [Authorize(Roles = "Librarian")]
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
            if (existingBook == updatedBookDetails)
            {
                return BadRequest("No updates found!please update some changes to update");
            }
            var bookDto = updatedBookDetails != null ? _mapper.Map<Book, BookDto>(updatedBookDetails) : null;
            if (bookDto != null)
                return Ok(bookDto);
            return BadRequest();
        }

        [HttpDelete("{bookId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            _logger.LogInformation($"Deleting a Book with {bookId}");
            var existingBook = await _bookRepository.GetBookById(bookId);
            if (existingBook == null)
            {
                return BadRequest("Book is not exist!");
            }
            var pendingBooksToBeReturn = await _returnRepository.GetPendingBookToBeReturn();
            var isIssuedBookPending = pendingBooksToBeReturn.FirstOrDefault(x => x.BookId == bookId);
            if (isIssuedBookPending != null)
            {
                return BadRequest($"Delete request for this book id: {bookId} is pending to return!");
            }
            var result = await _bookRepository.DeleteBookAsync(existingBook!);
            if (result != null)
                return NoContent();
            return BadRequest();
        }
    }
}