using DigitalBank.Core.Entities;

namespace DigitalBank.Core.Contracts
{
   public interface IAccount
   {
      decimal Balance { get; }
      ulong Number { get; }
      Owner Owner { get; set; }
      List<Transaction> Transactions { get; }

      bool Deposite(Amount amount, DateTime date, string? note);
      bool Withdraw(Amount amount, DateTime date, string? note);
   }
}