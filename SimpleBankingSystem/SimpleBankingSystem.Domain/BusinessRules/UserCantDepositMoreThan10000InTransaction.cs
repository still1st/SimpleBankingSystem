using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// A user cannot deposit more than $10,000 in a single transaction.
    /// </summary>
    public class UserCantDepositMoreThan10000InTransaction : BusinessRuleBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserCantDepositMoreThan10000InTransaction"/>
        /// </summary>
        /// <param name="transaction"></param>
        public UserCantDepositMoreThan10000InTransaction(Transaction transaction)
            : base(transaction)
        {

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
            if (Transaction.Type == TransactionType.Withdraw)
                return canExecute;

            var amount = Transaction.Amount;

            if (amount > 10000m)
                canExecute = false;

            if (!canExecute)
                AddError("Can't deposit more than $10,000 in a single transaction");

            return canExecute;
        }
    }
}
