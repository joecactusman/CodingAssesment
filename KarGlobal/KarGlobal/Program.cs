using System;
using System.Collections.Generic;
using System.Linq;

namespace KarGlobal
{
    class Program
    {
        static void Main(BankAccountTransaction bankAccountTransaction, int? sourceAccountId)
        {
            switch (bankAccountTransaction.TransactionType)
            {
                case TransactionTypeEnum.Transfer:
                    ProcessTransfer(bankAccountTransaction, sourceAccountId);
                    break;

                case TransactionTypeEnum.Deposit:
                    ProcessDeposit(bankAccountTransaction);
                    break;

                case TransactionTypeEnum.Withdrawal:
                    ProcessWithdrawal(bankAccountTransaction);
                    break;
            }
        }

        public static void ProcessTransfer(BankAccountTransaction bankAccountTransaction, int? souceAccountId)
        {
            List<Account> ownerAccountList = GetOwnerAccounts(bankAccountTransaction);

            var sourceAccount = ownerAccountList.Select(n => n.AccountId == souceAccountId);
            var targetAccount = ownerAccountList.Select(n => n.AccountId == bankAccountTransaction.TargetAccount.AccountId);


        }

        private static List<Account> GetOwnerAccounts(BankAccountTransaction bankAccountTransaction)
        {
            return Accounts.GetAccounts()
                            .Where(n => n.BankId == bankAccountTransaction.Bank.BankId)
                            .SelectMany(n => n.AccountOwners
                            .SelectMany(m => m.Accounts
                            .Where(l => l.OwnerId == bankAccountTransaction.OwnerId))).ToList();
        }

        public static void ProcessDeposit(BankAccountTransaction bankAccountTransaction)
        {

        }

        public static void ProcessWithdrawal(BankAccountTransaction bankAccountTransaction)
        {

        }
    }
}
