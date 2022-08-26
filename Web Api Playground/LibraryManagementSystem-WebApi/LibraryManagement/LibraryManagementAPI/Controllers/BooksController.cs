using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] BookDto book)
        {
            return Ok(await _bookRepository.AddBook(book));
        }
    }
}
