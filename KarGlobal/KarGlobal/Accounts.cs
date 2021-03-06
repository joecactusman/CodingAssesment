using System;
using System.Collections.Generic;
using System.Text;

namespace KarGlobal
{
    public class Accounts
    {
        public static List<Bank> GetAccounts()
        {
            return new List<Bank>
        {
            new Bank
            {
                BankName = "Example Bank 1",
                BankId = 5,
                AccountOwners = new List<AccountOwner>
                {
                    new AccountOwner
                    {
                        OwnerName = "John Fisherman",
                        Id = 202,
                        Accounts = new List<Account>
                        {
                            new CheckingAccount
                            {
                                OwnerId = 202,
                                AccountId = 501,
                                Balance = 1000,
                            },
                                new CorporateInvestmentAccount
                            {
                                AccountId = 605,
                                OwnerId = 202,
                                Balance = 10000,
                            },
                                new IndividualInvestmentAccount
                            {
                                AccountId = 703,
                                OwnerId = 202,
                                Balance = 600,
                            },
                        },
                    },
                     new AccountOwner
                    {
                        OwnerName = "Susan Fisherman",
                        Id = 205,
                        Accounts = new List<Account>
                        {
                            new CheckingAccount
                            {
                                AccountId = 845,
                                OwnerId = 205,
                                Balance = 3000,
                            },
                                new CorporateInvestmentAccount
                            {
                                AccountId = 662,
                                OwnerId = 205,
                                Balance = 14000,
                            },
                        },
                     }
                },
            },
             new Bank
            {
                BankName = "Example Bank 2",
                BankId = 9,
                AccountOwners = new List<AccountOwner>
                {
                    new AccountOwner
                    {
                        OwnerName = "Peter Banstead",
                        Id = 101,
                        Accounts = new List<Account>
                        {
                            new CheckingAccount
                            {
                                AccountId = 885,
                                OwnerId = 101,
                                Balance = 1000,
                            },
                                new CorporateInvestmentAccount
                            {
                                AccountId = 106,
                                OwnerId = 101,
                                Balance = 10000,
                            },
                                new IndividualInvestmentAccount
                            {
                                AccountId = 704,
                                OwnerId = 101,
                                Balance = 600,
                            },
                        },
                    },
                     new AccountOwner
                    {
                        OwnerName = "Rachelle Johnson",
                        Id = 102,
                        Accounts = new List<Account>
                        {
                            new CheckingAccount
                            {
                                AccountId = 845,
                                OwnerId = 102,
                                Balance = 3000,
                            },
                                new IndividualInvestmentAccount
                            {
                                AccountId = 662,
                                OwnerId = 102,
                                Balance = 14000,
                            },
                        },
                     }
                },
            }
        };
        }
    }
}
