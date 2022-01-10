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
            var result = Program.Main(data);

            // assert
            Assert.True(result.SingleOrDefault().Balance == startingBalance - data.TransactionAmount);
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
            var result = Program.Main(data);

            // assert
            Assert.True(result.SingleOrDefault().Balance == startingBalance + data.TransactionAmount);
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
                TransactionAmount = 600,
                SourceAccountId = 605
            };

            var ownerAccounts = Program.GetOwnerAccounts(data);
            var startingTargetBalance = ownerAccounts.Where(n => n.AccountId == data.TargetAccount.AccountId).SingleOrDefault().Balance;
            var startingSourceBalance = ownerAccounts.Where(n => n.AccountId == data.SourceAccountId).SingleOrDefault().Balance;

            // act
            var result = Program.Main(data);

            // assert
            var endingTargetBalance = result.Where(n => n.AccountId == data.TargetAccount.AccountId).SingleOrDefault().Balance;
            var endingSourceBalance = result.Where(n => n.AccountId == data.SourceAccountId).SingleOrDefault().Balance;

            Assert.True(
                (endingTargetBalance == startingTargetBalance + data.TransactionAmount) && 
                (endingSourceBalance == startingSourceBalance - data.TransactionAmount)
                );
        }
    }
}
