namespace SmartBank.Core
{
    public class Account:IAccount
    {
        private decimal _balance;
        ulong Number { get; }
        public decimal Balance 
        {
            get
            {
                return _balance;
            }
        }
        public Account()
        {
                
        }

    }
}
