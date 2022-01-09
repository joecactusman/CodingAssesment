using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KarGlobal
{
    public enum TransactionTypeEnum
    {
        /// <summary>
        /// Withdrawal.
        /// </summary>
        [Description("Withdrawal")]
        Withdrawal = 1,

        /// <summary>
        /// Deposit.
        /// </summary>
        [Description("Deposit")]
        Deposit = 2,

        /// <summary>
        /// Transfer.
        /// </summary>
        [Description("Transfer")]
        Transfer = 3,
    }
}
