using System;
using System.Collections.Generic;
using System.Linq;

namespace KarGlobal
{
    public class Program
    {
        public static List<Account> Main(BankAccountTransactionRequest bankAccountTransactionRequest)
        {
            var ownerAccountList = GetOwnerAccounts(bankAccountTransactionRequest);
            var targetAccount = ownerAccountList.Where(n => n.AccountId == bankAccountTransactionRequest.TargetAccount.AccountId).SingleOrDefault();

            switch (bankAccountTransactionRequest.TransactionType)
            {
                case TransactionTypeEnum.Transfer:
                    return ProcessTransfer(bankAccountTransactionRequest, ownerAccountList, targetAccount);

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

        public static List<Account> ProcessTransfer(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount)
        {
            var sourceAccount = ownerAccounts.Where(n => n.AccountId == bankAccountTransaction.SourceAccountId).SingleOrDefault();
            var accounts = new List<Account>();

            var withdrawal = ProcessWithdrawal(
                new BankAccountTransactionRequest
                {
                    Bank = bankAccountTransaction.Bank,
                    TargetAccount = sourceAccount,
                    TransactionAmount = bankAccountTransaction.TransactionAmount
                }, ownerAccounts, sourceAccount);

            var deposit = ProcessDeposit(bankAccountTransaction, ownerAccounts, targetAccount);

            accounts.Add(withdrawal.SingleOrDefault());
            accounts.Add(deposit.SingleOrDefault());


            if (withdrawal != null && deposit != null)
            {
                return accounts;
            }

            return null;
        }

        public static List<Account> ProcessDeposit(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount)
        {
            var accounts = new List<Account>();

            var isValidTransaction = ValidateTransaction(targetAccount, bankAccountTransaction);

            if (isValidTransaction)
            {
                targetAccount.Balance += bankAccountTransaction.TransactionAmount;
                accounts.Add(targetAccount);
                return accounts;
            }

            return null;
        }

        public static List<Account> ProcessWithdrawal(BankAccountTransactionRequest bankAccountTransaction, List<Account> ownerAccounts, Account targetAccount)
        {
            var accounts = new List<Account>();

            var isValidTransaction = ValidateTransaction(targetAccount, bankAccountTransaction);

            if (isValidTransaction)
            {
                targetAccount.Balance -= bankAccountTransaction.TransactionAmount;
                accounts.Add(targetAccount);
                return accounts;
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
