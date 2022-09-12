using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// This method is use to add new book or update the book stock if book name is matching with existing books
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>Book</returns>
        public async Task<Book?> AddBookAsync(Book book)
        {
            Book? bookToBeAdd = await _bookRepository.GetBookByBookName(book.BookName);
            if (book != null && bookToBeAdd != null && book.BookName == bookToBeAdd.BookName)
            {
                var incrementResult = IncrementBookStock(bookToBeAdd);
                bookToBeAdd = incrementResult;
            }
            else if (book != null && bookToBeAdd == null)
            {
                bookToBeAdd = AddInitialBookStock(book);
            }
            if (bookToBeAdd != null)
            {
                var bookAddedDetails = await _bookRepository.AddBookAsync(bookToBeAdd);
                return bookAddedDetails;
            }
            return null;
        }

        /// <summary>
        /// This method is use to get all books
        /// </summary>
        /// <returns>IEnumerable<Books></returns>
        public async Task<IEnumerable<Book>?> GetBooksAsync()
        {
            var booksResult = await _bookRepository.GetBooksAsync();
            if (booksResult != null)
                return booksResult;
            return null;
        }

        /// <summary>
        /// This method is use to get the book details by book book name
        /// </summary>
        /// <param name="bookName">BookName</param>
        /// <returns>Book</returns>
        public async Task<Book?> GetBookByNameAsync(string bookName)
        {
            var bookByName = await _bookRepository.GetBookByBookName(bookName);
            if (bookByName != null)
                return bookByName;
            return null;
        }

        /// <summary>
        /// This method is use to get book details by passing book id
        /// </summary>
        /// <param name="bookId">bookid</param>
        /// <returns>Book</returns>
        public async Task<Book?> GetBookByBookId(int bookId)
        {
            var bookByBookId = await _bookRepository.GetBookById(bookId);
            if (bookByBookId != null)
                return bookByBookId;
            return null;
        }

        /// <summary>
        /// This method is use to update existing book details
        /// </summary>
        /// <param name="book">book</param>
        /// <param name="bookId">bookid</param>
        /// <returns>updated book</returns>
        public async Task<Book?> UpdateBooksAsync(Book book, int bookId)
        {
            var existingBook = await _bookRepository.GetBookById(bookId);
            Book? updatedBookDetails = null;
            if (existingBook != null)
            {
                existingBook.BookId = book.BookId;
                existingBook.AuthorName = book.AuthorName;
                existingBook.BookEdition = book.BookEdition;
                existingBook.BookName = book.BookName;
                existingBook.Isbn = book.Isbn;
                updatedBookDetails = await _bookRepository.UpdateBookAsync(existingBook);
                return updatedBookDetails;
            }
            return updatedBookDetails;
        }

        public async Task<Book?> DeleteBookAsync(int bookId)
        {
            var existingBook = await _bookRepository.GetBookById(bookId);
            if (existingBook != null)
            {
                var deletedBook = await _bookRepository.DeleteBookAsync(existingBook);
                return deletedBook;
            }
            return null;
        }

        /// <summary>
        /// This method is use to initialize the new book stock
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>Book</returns>
        public Book? AddInitialBookStock(Book? book)
        {
            if (book != null)
            {
                var bookRecord = new Book()
                {
                    AuthorName = book.AuthorName,
                    BookEdition = book.BookEdition ?? "Default",
                    BookId = book.BookId,
                    BookName = book.BookName,
                    Isbn = book.Isbn,
                    StockAvailable = 1
                };
                return bookRecord;
            }
            return null;
        }

        /// <summary>
        /// This method is use to increment the available book stock
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>Book</returns>
        public Book? IncrementBookStock(Book? book)
        {
            if (book != null)
            {
                book.StockAvailable += 1;
                return book;
            }
            return null;
        }
    }
}