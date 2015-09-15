using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// An account cannot have less than $100 at any time in an account.
    /// </summary>
    public class AccountCantHaveLess100 : BusinessRuleBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AccountCantHaveLess100"/>
        /// </summary>
        /// <param name="transaction">Transaction</param>
        public AccountCantHaveLess100(Transaction transaction)
            : base(transaction)
        {

        }

        /// <summary>
        /// Checks that the transaction can be executed
        /// </summary>
        /// <returns>TRUE - if transaction is correct, FALSE - else</returns>
        public override Boolean CanExecuteTransaction()
        {
            var account = Transaction.Account;
            var balance = account.Balance;
            var amount = Transaction.Amount;
            var type = Transaction.Type;

            var canExecute = true;
            if (type == TransactionType.Deposit)
            {
                if (balance + amount < 100m)
                    canExecute = false;
            }
            else if(type == TransactionType.Withdraw)
            {
                if (balance - amount < 100m)
                    canExecute = false;
            }

            if(!canExecute)
                Errors.Add(new BusinessError(Transaction.TransactionId, "An account can't have less than $100 at any time"));

            return canExecute;
        }
    }
}
