using AutoMapper;
using FluentAssertions;
using LibraryManagement.Api.Controllers.V1;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace LibraryManagementSystem.Tests.Controller
{
    public class BooksControllerTests
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly Mock<IBookService> _booksService;

        public BooksControllerTests()
        {
            _booksService = new Mock<IBookService>();
            _mapper = null;
            _logger = new NullLogger<BooksController>();
        }

        [Fact]
        public async Task GetBooks_ShouldReturnOkResult()
        {
            //var books = new BooksController();
            var mockData = _booksService.Setup(x => x.GetBooksAsync()).Returns(Task.FromResult<IEnumerable<Book>>(new List<Book>()));

            //Act
            BooksController booksController = new BooksController(_booksService.Object, _mapper, _logger);
            IActionResult result = await booksController.GetBooks();

            //Assert
            //Assert.NotEmpty(((result as OkObjectResult).Value as IEnumerable<Book>));
            Assert.Equal(0, ((result as OkObjectResult).Value as IEnumerable<Book>).Count());
        }
    }
}