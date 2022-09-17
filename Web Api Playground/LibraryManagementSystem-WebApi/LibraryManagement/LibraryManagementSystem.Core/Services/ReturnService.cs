using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IIssueService _issueService;
        private readonly IIssueRepository _issueRepository;
        private readonly IBookService _bookService;
        private readonly IPenaltyService _penaltyService;

        public ReturnService(IReturnRepository returnRepository, IBookRepository bookRepository, IIssueService issueService, IIssueRepository issueRepository, IBookService bookService, IPenaltyService penaltyService)
        {
            _returnRepository = returnRepository;
            _bookRepository = bookRepository;
            _issueService = issueService;
            _issueRepository = issueRepository;
            _bookService = bookService;
            _penaltyService = penaltyService;
        }

        public async Task<Return?> AddReturnAsync(Return returnDetails, short issueId)
        {
            var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
            var bookDetails = await _bookRepository.GetBookById(issueDetails.BookId);
            if (issueDetails == null)
            {
                return null;
            }
            var isPenalty = await _penaltyService.IsPenalty(issueId);
            var penaltyData = await _penaltyService.GetPenaltyByIdAsync(issueId);

            if (penaltyData == null || penaltyData.PenaltyPaidStatus == true)
            {
                var returnRecord = new Return();
                returnRecord.ExpiryDate = issueDetails.ExpiryDate;
                returnRecord.IssueDate = issueDetails.IssueDate;
                returnRecord.BookId = issueDetails.BookId;
                returnRecord.ReturnDate = DateTime.UtcNow;

                await _issueRepository.DeleteIssueAsync(issueDetails);
                bookDetails.StockAvailable += 1;
                var returnRecordResult = await _returnRepository.AddReturnAsync(returnRecord, bookDetails);
                if (returnRecordResult != null)
                    return returnRecord;
            }
            return null;
        }

        public async Task<IEnumerable<Return>?> GetReturnAsync()
        {
            var booksReturns = await _returnRepository.GetReturnAsync();
            if (booksReturns != null)
                return booksReturns;
            return null;
        }

        public async Task<Return?> GetReturnByIdAsync(int returnId)
        {
            var bookReturn = await _returnRepository.GetReturnByIdAsync(returnId);
            if (bookReturn != null)
                return bookReturn;
            return null;
        }

        public async Task<Return?> UpdateReturnAsync(int returnId, Return returnDetails)
        {
            var returnRecord = await GetReturnByIdAsync(returnId);
            if (returnRecord != null)
            {
                returnRecord.ReturnId = returnId;
                returnRecord.ExpiryDate = returnDetails.ExpiryDate;
                returnRecord.IssueDate = returnDetails.IssueDate;
                returnRecord.BookId = returnDetails.BookId;
                returnRecord.ReturnDate = DateTime.UtcNow;

                var updatedBookReturns = await _returnRepository.UpdateReturnAsync(returnRecord);
                if (updatedBookReturns != null)
                    return updatedBookReturns;
            }
            return null;
        }

        public async Task<Return?> DeleteReturnAsync(int returnId)
        {
            var returnRecord = await GetReturnByIdAsync(returnId);
            if (returnRecord != null)
            {
                var deletedBookReturn = await _returnRepository.DeleteReturnAsync(returnRecord);
                if (deletedBookReturn != null)
                    return deletedBookReturn;
            }
            return null;
        }
    }
}