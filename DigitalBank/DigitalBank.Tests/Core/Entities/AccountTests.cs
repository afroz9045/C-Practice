
using DigitalBank.Core.Contracts;
using DigitalBank.Core.Services;

namespace DigitalBank.Tests.Core.Entities
{
   [TestClass]
   public class AccountTests
   {
        // Positive Test Case
        //[TestMethod]
        public void Account_ValidOpeningBalance_ShouldSucceed()
        {
            // Arrange
            var owner = new Owner("Avishek", "Kumar");
            var openingBalance = new Amount { Value = 500, Currency = CurrencyType.INR };
            ulong expectedAccountNumber = 1000000000000000;

            // Act
            var account = new Account(owner, openingBalance);

            // Assert
            Assert.AreEqual<decimal>(openingBalance.Value, account.Balance);
            Assert.AreEqual("Initial amount.", account.Transactions.First().Note);
            Assert.AreEqual(expectedAccountNumber, account.Number);
        }

        // Negative Test Case
        [TestMethod]
      public void Account_InvalidOpeningBalance_ShouldThrowError()
      {
         // Arrange
         var owner = new Owner("Avishek", "Kumar");
         var openingBalance = new Amount { Value = 300, Currency = CurrencyType.INR };

         // Act & Assert
         Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Account(owner, openingBalance));
      }

      // Positive Test Case
      [TestMethod]
      public void Deposit_ValidAmount_ShouldSucceed()
      {
         // Arrange
         decimal depositAmount = 300m;
         decimal expectedBalance = 800;
         var account = new Account(new Owner("Avishek", "Kumar"), new Amount { Value = 500, Currency = CurrencyType.INR });

         // Act
         var depositResult = account.Deposite(new Amount { Value = depositAmount, Currency = CurrencyType.INR }, DateTime.Now, "Depositing valid amount.");

         // Assert
         Assert.IsTrue(depositResult);
         Assert.AreEqual(expectedBalance, account.Balance);
      }

      // Negative Test Case
      [DataTestMethod]
      [DataRow(0d)]
      [DataRow(-1.5)]
      public void Deposit_AmountZeroOrLess_ShouldThrowError(double depositAmount)
      {
         // Arrange
         var account = new Account(new Owner("Avishek", "Kumar"), new Amount { Value = 500, Currency = CurrencyType.INR });

         // Act & Assert
         Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Deposite(new Amount { Value = (decimal)depositAmount, Currency = CurrencyType.INR }, DateTime.Now, "Depositing valid amount."));
      }

       [TestMethod]
        public void Withdrawl_ValidAmount_ShouldSucceed()
        {
            var withDrawlAmount = 350;
            var account = new Account(new Owner("Avishek", "Kumar"), new Amount { Value = 500, Currency = CurrencyType.INR });
            var balance = 150;
            var withdrawlReturn = account.Withdraw(new Amount { Value = withDrawlAmount, Currency = CurrencyType.INR }, DateTime.Now, "withdrawl");
            Assert.IsTrue(withdrawlReturn);
            Assert.AreEqual(balance, account.Balance);
        }
        [DataTestMethod]
        [DataRow(2500)]
        [DataRow(-100)]
        //Negative Test Case
        public void Withdrawl_InvalidAmount_ShouldThrowError(int withdrawlAmount)
        {
            
            var account = new Account(new Owner("Avishek", "Kumar"), new Amount { Value = 500, Currency = CurrencyType.INR });
            if (withdrawlAmount>=0)
            {
                Assert.ThrowsException<InvalidOperationException>(() => account.Withdraw(new Amount { Value = withdrawlAmount, Currency = CurrencyType.INR }, DateTime.Now, "withdraw"));
            }
            else { 
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw(new Amount { Value = withdrawlAmount, Currency = CurrencyType.INR }, DateTime.Now, "withdraw"));
            }
        }


        [TestMethod]
        public void TransactionHistory_ValidData_ShouldSucceed()
        {
            // Arrange
            var description = $"All transaction history for {DateTime.Now.ToShortDateString()}";
            //var header = "Date\t\tAmount\tBalance\tType\tNote";
            var account = new Account(new Owner("Avishek", "Kumar"), new Amount { Value = 500, Currency = CurrencyType.INR });
            ITransactionService transactionService = new TransactionService();

            // Act
            var transactionHistory = transactionService.GetTransactionHistory(account.Transactions);
            var matchingNoteFound = transactionHistory.TransactionHistory.Contains("Initial amount.", StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual(description, transactionHistory.Description);
            Assert.IsTrue(matchingNoteFound);
        }

        [TestMethod]

        public void TransactionHistoryByType_ValidData_ShouldSucceed()
        {
            //Arrange

            //Act
        }
    }
}
