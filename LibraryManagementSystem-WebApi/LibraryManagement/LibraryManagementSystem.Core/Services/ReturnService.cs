using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class ReturnService : IReturnService
    {
        public (Return?, Book?) AddReturn(Return returnDetails, short issueId, Penalty? isPenalty, Book? bookDetails, Issue issueDetails)
        {
            if (isPenalty == null || isPenalty.PenaltyPaidStatus == true)
            {
                var returnRecord = new Return();
                returnRecord.ExpiryDate = issueDetails.ExpiryDate;
                returnRecord.IssueId = issueId;
                returnRecord.IssueDate = issueDetails.IssueDate;
                returnRecord.BookId = issueDetails.BookId;
                returnRecord.ReturnDate = DateTime.UtcNow;

                bookDetails!.StockAvailable += 1;
                return (returnRecord, bookDetails);
            }
            return (null, null);
        }

        public IEnumerable<PendingReturnDto> IsStudentOrStaff(IEnumerable<PendingBookReturnDto> returns)
        {
            List<PendingReturnDto> result = new List<PendingReturnDto>();
            foreach (var returnRecord in returns)
            {
                PendingReturnDto returnDto = new();
                returnDto.IssueId = returnRecord.IssueId;
                returnDto.BookId = returnRecord.BookId;
                returnDto.Id = string.IsNullOrEmpty(returnRecord.StaffId) ? returnRecord.StudentId.ToString() : returnRecord.StaffId;
                returnDto.IssuedTo = string.IsNullOrEmpty(returnRecord.StaffId) ? "Student" : "Staff";
                result.Add(returnDto);
            }
            return result;
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