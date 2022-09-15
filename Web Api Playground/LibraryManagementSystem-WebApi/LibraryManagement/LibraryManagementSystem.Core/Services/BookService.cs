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
        public Book? AddBookAsync(Book book, Book? existingBook)
        {
            if (book != null && existingBook != null && book.BookName == existingBook.BookName)
            {
                if (existingBook.BookEdition != book.BookEdition)
                {
                    var newBook = AddInitialBookStock(book);
                    return newBook;
                }
                var incrementResult = IncrementBookStock(existingBook);
                existingBook = incrementResult;
                return existingBook;
            }
            else if (book != null && existingBook == null)
            {
                var newBook = AddInitialBookStock(book);
                return newBook;
            }
            return null;
        }

        /// <summary>
        /// This method is use to update existing book details
        /// </summary>
        /// <param name="book">book</param>
        /// <param name="bookId">bookid</param>
        /// <returns>updated book</returns>
        public Book? UpdateBooksAsync(Book book, Book? existingBook)
        {
            Book? updatedBookDetails = null;
            if (existingBook != null)
            {
                existingBook.BookId = book.BookId;
                existingBook.AuthorName = book.AuthorName;
                existingBook.BookEdition = book.BookEdition;
                existingBook.BookName = book.BookName;
                existingBook.Isbn = book.Isbn;
                return updatedBookDetails;
            }
            return updatedBookDetails;
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