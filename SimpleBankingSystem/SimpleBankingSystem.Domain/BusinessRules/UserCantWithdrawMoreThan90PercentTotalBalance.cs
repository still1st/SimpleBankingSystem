using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// A user cannot withdraw more than 90% of their total balance
    /// from an account in a single transaction.
    /// </summary>
    public class UserCantWithdrawMoreThan90PercentTotalBalance : BusinessRuleBase
    {
        /// <summary>
        /// Collection of the accounts of the user
        /// </summary>
        private IEnumerable<Account> _userAccounts;

        /// <summary>
        /// Initializes a new instance of <see cref="UserCantWithdrawMoreThan90PercentTotalBalance"/>
        /// </summary>
        /// <param name="userAccounts">Collection of the accounts of the user</param>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        public UserCantWithdrawMoreThan90PercentTotalBalance(IEnumerable<Account> userAccounts, Transaction transaction)
            : base(transaction)
        {
            _userAccounts = userAccounts;
        }

        /// <summary>
        /// Checks that the transaction can be executed
        /// </summary>
        /// <returns>TRUE - if transaction is correct, FALSE - else</returns>
        public override Boolean CanExecuteTransaction()
        {
            var canExecute = true;

            // if transaction type equals deposit
            // then just return true
            if (Transaction.Type == TransactionType.Deposit)
                return canExecute;

            var amount = Transaction.Amount;
            var totalBalance = _userAccounts.Sum(x => x.Balance);

            var percentage = amount / totalBalance * 100;
            if (percentage > 90)
                canExecute = false;

            if (!canExecute)
                Errors.Add(new BusinessError(Transaction.TransactionId, "Can't withdraw more than 90% of their total balance from an account in a single transaction."));

            return canExecute;
        }
    }
}
