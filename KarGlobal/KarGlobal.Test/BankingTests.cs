using System.Linq;
using Xunit;
using System;

namespace KarGlobal.Test
{
    public class BankingTests
    {
        [Fact]
        public void WithdrawalTest()
        {
            // arrange
            var data = new BankAccountTransactionRequest
            {
                Bank = new Bank
                {
                    BankId = 5,
                },
                OwnerId = 202,
                TransactionType = TransactionTypeEnum.Withdrawal,
                TargetAccount = new CheckingAccount
                {
                    AccountId = 501
                },
                TransactionAmount = 600
            };

            var ownerAccounts = Program.GetOwnerAccounts(data);
            var startingBalance = ownerAccounts.Where(n => n.AccountId == data.TargetAccount.AccountId).SingleOrDefault().Balance;

            // act
            var result = Program.Main(data, null);

            // assert
            Assert.True(result.Balance == startingBalance - data.TransactionAmount);
        }

        [Fact]
        public void DepositTest()
        {
            // arrange
            var data = new BankAccountTransactionRequest
            {
                Bank = new Bank
                {
                    BankId = 5,
                },
                OwnerId = 202,
                TransactionType = TransactionTypeEnum.Deposit,
                TargetAccount = new CheckingAccount
                {
                    AccountId = 501
                },
                TransactionAmount = 600
            };

            var ownerAccounts = Program.GetOwnerAccounts(data);
            var startingBalance = ownerAccounts.Where(n => n.AccountId == data.TargetAccount.AccountId).SingleOrDefault().Balance;

            // act
            var result = Program.Main(data, null);

            // assert
            Assert.True(result.Balance == startingBalance + data.TransactionAmount);
        }
        [Fact]
        public void TransferTest()
        {
            // arrange
            var data = new BankAccountTransactionRequest
            {
                Bank = new Bank
                {
                    BankId = 5,
                },
                OwnerId = 202,
                TransactionType = TransactionTypeEnum.Transfer,
                TargetAccount = new CheckingAccount
                {
                    AccountId = 501
                },
                TransactionAmount = 400
            };

            var ownerAccounts = Program.GetOwnerAccounts(data);
            var startingBalance = ownerAccounts.Where(n => n.AccountId == data.TargetAccount.AccountId).SingleOrDefault().Balance;

            // act
            var result = Program.Main(data, 703);

            // assert
            Assert.True(result.Balance == startingBalance + data.TransactionAmount);
        }
    }
}
