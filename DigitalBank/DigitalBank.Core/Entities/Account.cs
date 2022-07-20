using DigitalBank.Core.Contracts;

namespace DigitalBank.Core.Entities
{
    public enum CurrencyType : int
    {
        INR = 1,
        USD,
        GBP
    }
    public class Account : IAccount
    {
        public ulong Number { get; }
        public Owner Owner { get; set; }
        public bool? IsNri { get; set; }
        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }
        private static ulong _accountNumberSeed = 1000000000000000;
        private decimal _balance;
        public List<Transaction> Transactions { get; internal set; } = new List<Transaction>();

        public Account(Owner owner, Amount initialAmount, bool? isNri = null)
        {
            if (initialAmount.Value < 500)
            {
                throw new ArgumentOutOfRangeException(nameof(initialAmount), "Minimum opening balance need to be 500 or more.");
            }
            Owner = owner;
            Number = _accountNumberSeed;
            _accountNumberSeed++;
            Deposite(initialAmount, DateTime.Now, "Initial amount.");
            IsNri = isNri;
        }

        public bool Deposite(Amount amount, DateTime date, string? note)
        {
            if (amount.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposite amount must be positive.");
            }
            _balance += amount.Value;
            Transactions.Add(new Transaction(amount, date, note, TransactionType.Credit));
            return true;
        }

        public bool Withdraw(Amount amount, DateTime date, string? note)
        {
            if (amount.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive.");
            }
            if (_balance - amount.Value < 0)
            {
                throw new InvalidOperationException($"Insufficient funds for this withdrawal amount. Available balance is: {Balance}");
            }
            _balance -= amount.Value;
            amount.Value = -amount.Value;
            Transactions.Add(new Transaction(amount, date, note, TransactionType.Debit));
            return true;
        }
    }
}
