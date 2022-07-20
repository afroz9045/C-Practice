using DigitalBank.Core.Contracts;
using DigitalBank.Core.Entities;
using System.Text;

namespace DigitalBank.Core.Services
{
   public class TransactionService : ITransactionService
   {
      public (string Description, string TransactionHistory) GetTransactionHistory(List<Transaction> transactions)
      {
         var description = $"All transaction history for {DateTime.Now.ToShortDateString()}";
         decimal balance = 0;
         var transactionHistory = new StringBuilder();
         transactionHistory.AppendLine("Date\t\tAmount\tBalance\tType\tNote");
         foreach (var transaction in transactions)
         {
            balance += transaction.Amount.Value;
            var note = transaction.Note?.ToUpper() ?? "No note.";
            transactionHistory.AppendLine($"{transaction.Date.ToShortDateString()}\t{transaction.Amount.Value}\t{balance}\t{transaction.Type.ToString()}\t{note.ToUpper()}");
         }
         return (description, transactionHistory.ToString());
      }

      public (string Description, string TransactionHistory) GetTransactionHistoryByType(List<Transaction> transactions, TransactionType transactionType)
      {
         var description = $"{transactionType.ToString()} transaction history for {DateTime.Now.ToShortDateString()}";
         var transactionsByType = transactions.Where(t => t.Type == transactionType).Select(at => new { TransactionDate = at.Date, at.Amount, at.Note }).ToList();
         var transactionHistory = new StringBuilder();
         transactionHistory.AppendLine("Date\t\tAmount\tNote");
         foreach (var transaction in transactionsByType)
         {
            var note = transaction.Note?.ToUpper() ?? "No note.";
            transactionHistory.AppendLine($"{transaction.TransactionDate.ToShortDateString()}\t{transaction.Amount.Value}\t{note.ToUpper()}");
         }
         return (description, transactionHistory.ToString());
      }
   }
}
