using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRespository;

        public BooksController(IBookRepository bookRespository)
        {
            _bookRespository = bookRespository;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BooksDto>>> AddBook([FromBody] BooksDto book)
        {
            var bookAddResult = await _bookRespository.AddBookAsync(book);
            return Ok(bookAddResult);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(GetBooks());
        }


    }
}
