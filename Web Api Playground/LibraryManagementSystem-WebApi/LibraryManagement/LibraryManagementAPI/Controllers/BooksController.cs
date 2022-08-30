using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] BookVm bookVm)
        {
            var book = new Book
            {
                AuthorName = bookVm.AuthorName,
                BookEdition = bookVm.BookEdition,
                BookName = bookVm.BookName,
                Isbn = bookVm.Isbn
            };
            return Ok(await _bookRepository.AddBookAsync(book));
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            return Ok(await _bookRepository.GetBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            var result = _bookRepository.GetBookById(id);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookDetails([FromBody] BookDto book, int id)
        {
            var result = _bookRepository.UpdateBookAsync(book, id);
            if (result != null)
                return Ok(await result);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = _bookRepository.DeleteBookAsync(id);
            if (result != null)
                return Ok(await result);
            return NotFound();
        }
    }
}