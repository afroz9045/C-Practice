using DigitalBank.Core.Entities;

namespace DigitalBank.Core.Contracts
{
   public interface ITransactionService
   {
      (string Description, string TransactionHistory) GetTransactionHistory(List<Transaction> transactions);
      (string Description, string TransactionHistory) GetTransactionHistoryByType(List<Transaction> transactions, TransactionType transactionType);
   }
}