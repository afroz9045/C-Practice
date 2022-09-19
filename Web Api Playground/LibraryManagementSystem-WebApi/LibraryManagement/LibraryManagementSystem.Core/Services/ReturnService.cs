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
        private readonly IPenaltyRepository _penaltyRepository;

        public ReturnService(IReturnRepository returnRepository, IBookRepository bookRepository, IIssueService issueService, IIssueRepository issueRepository, IBookService bookService, IPenaltyService penaltyService, IPenaltyRepository penaltyRepository)
        {
            _returnRepository = returnRepository;
            _bookRepository = bookRepository;
            _issueService = issueService;
            _issueRepository = issueRepository;
            _bookService = bookService;
            _penaltyService = penaltyService;
            _penaltyRepository = penaltyRepository;
        }

        public (Return?, Book?) AddReturn(Return returnDetails, short issueId, Penalty? isPenalty, Book? bookDetails, Issue issueDetails)
        {
            if (isPenalty == null || isPenalty.PenaltyPaidStatus == true)
            {
                var returnRecord = new Return();
                returnRecord.ExpiryDate = issueDetails.ExpiryDate;
                returnRecord.IssueDate = issueDetails.IssueDate;
                returnRecord.BookId = issueDetails.BookId;
                returnRecord.ReturnDate = DateTime.UtcNow;

                bookDetails!.StockAvailable += 1;
                return (returnRecord, bookDetails);
            }
            return (null, null);
        }

        public Return? UpdateReturnAsync(int returnId, Return? existingReturnDetails, Return returnDetailsToBeUpdate)
        {
            if (existingReturnDetails != null)
            {
                existingReturnDetails.ReturnId = returnId;
                existingReturnDetails.ExpiryDate = returnDetailsToBeUpdate.ExpiryDate;
                existingReturnDetails.IssueDate = returnDetailsToBeUpdate.IssueDate;
                existingReturnDetails.BookId = returnDetailsToBeUpdate.BookId;
                existingReturnDetails.ReturnDate = DateTime.UtcNow;

                return existingReturnDetails;
            }
            return null;
        }
    }
}