using System;
using System.Collections.Generic;
using System.Text;

namespace KarGlobal
{
    public class BankAccountTransaction
    {
        public Bank Bank { get; set; }

        public Account TargetAccount { get; set; }

        public int OwnerId { get; set; }

        public double TransactionAmount { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public int? SourceAccountId { get; set; }
    }

    public class Bank
    {
        public string BankName { get; set; }
        public int BankId { get; set; }
        public List<AccountOwner> AccountOwners { get; set; }
    }

    public class AccountOwner
    {
        public string OwnerName { get; set; }

        public int Id { get; set; }

        public List<Account> Accounts { get; set; }
    }

    public class Account
    {
        public string OwnerName { get; set; }

        public int OwnerId { get; set; }
        
        public double Balance { get; set; }

        public int AccountId { get; set; }
    }

    public class CheckingAccount : Account
    {

    }

    public class InvestmentAccount : Account
    {

    }

    public class IndividualInvestmentAccount : InvestmentAccount
    {
        public int WithdrawalLimit = 500;
    }

    public class CorporateInvestmentAccount : InvestmentAccount
    {

    }

    public class Transaction
    {
        public double TransactionAmount { get; set; }
    }

    public class Deposit
    {

    }

    public class Withdrawal
    {

    }

    public class Transfer
    {
        public int SourceAccountId { get; set; }

        public int DestinationAccountId { get; set; }
    }
}
