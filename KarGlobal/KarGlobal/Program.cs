using System;
using System.Collections.Generic;
using System.Linq;

namespace KarGlobal
{
    public class Program
    {
        public static Account Main(BankAccountTransactionRequest bankAccountTransactionRequest, int? sourceAccountId)
        {
            var ownerAccountList = GetOwnerAccounts(bankAccountTransactionRequest);
            var targetAccount = ownerAccountList.Where(n => n.AccountId == bankAccountTransactionRequest.TargetAccount.AccountId).SingleOrDefault();

            switch (bankAccountTransactionRequest.TransactionType)
            {
                case TransactionTypeEnum.Transfer:
                    return ProcessTransfer(bankAccountTransactionRequest, ownerAccountList, targetAccount, sourceAccountId);

                case TransactionTypeEnum.Deposit:
                    return ProcessDeposit(bankAccountTransactionRequest, ownerAccountList, targetAccount);

                case TransactionTypeEnum.Withdrawal:
                    return ProcessWithdrawal(bankAccountTransactionRequest, ownerAccountList, targetAccount);
                default:
                    return null;
            }
        }

        public static List<Account> GetOwnerAccounts(BankAccountTransactionRequest bankAccountTransaction)
        {
            return Accounts.GetAccounts()
                            .Where(n => n.BankId == bankAccountTransaction.Bank.BankId)
                            .SelectMany(n => n.AccountOwners
                            .SelectMany(m => m.Accounts
                            .Where(l => l.OwnerId == bankAccountTransaction.OwnerId))).ToList();
        }

        public static Account ProcessTransfer(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount, int? souceAccountId)
        {
            var sourceAccount = ownerAccounts.Where(n => n.AccountId == souceAccountId).SingleOrDefault();

            var withdrawal = ProcessWithdrawal(
                new BankAccountTransactionRequest
                {
                    Bank = bankAccountTransaction.Bank,
                    TargetAccount = sourceAccount,
                    TransactionAmount = bankAccountTransaction.TransactionAmount
                }, ownerAccounts, sourceAccount);

            var deposit = ProcessDeposit(bankAccountTransaction, ownerAccounts, targetAccount);

            if (withdrawal != null && deposit != null)
            {
                return targetAccount;
            }

            return null;
        }

        public static Account ProcessDeposit(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount)
        {
            var isValidTransaction = ValidateTransaction(targetAccount, bankAccountTransaction);

            if (isValidTransaction)
            {
                targetAccount.Balance += bankAccountTransaction.TransactionAmount;
                return targetAccount;
            }

            return null;
        }

        public static Account ProcessWithdrawal(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount)
        {
            var isValidTransaction = ValidateTransaction(targetAccount, bankAccountTransaction);

            if (isValidTransaction)
            {
                targetAccount.Balance -= bankAccountTransaction.TransactionAmount;
                return targetAccount;
            }

            return null;
        }

        private static bool ValidateTransaction(Account targetAccount, BankAccountTransactionRequest bankAccountTransaction)
        {
            if (targetAccount is ITransactionLimitAccount)
            {
                if (bankAccountTransaction.TransactionAmount > 500)
                {
                    return false;
                };
            }

            if (bankAccountTransaction.TransactionType == TransactionTypeEnum.Withdrawal)
            {
                if (targetAccount.Balance < bankAccountTransaction.TransactionAmount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
