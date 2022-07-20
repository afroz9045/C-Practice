using System.Text;

namespace DigitalBank.Core.Entities
{
   public struct Amount
   {
      public decimal Value;
      public CurrencyType Currency;
   }
   public enum TransactionType : short
   {
      Credit = 1,
      Debit
   }
   public class Transaction
   {
      public Amount Amount;
      public DateTime Date { get; set; }
      public string? Note { get; set; }
      public TransactionType Type { get; set; }
      public Transaction(Amount amount, DateTime date, string? note, TransactionType type)
      {
         Amount = amount;
         Date = date;
         Note = note;
         Type = type;
      }
   }
}
