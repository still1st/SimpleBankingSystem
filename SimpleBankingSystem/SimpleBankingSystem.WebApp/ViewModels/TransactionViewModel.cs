using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.WebApp.ViewModels
{
    /// <summary>
    /// ViewModel for transaction
    /// </summary>
    public class TransactionViewModel
    {
        public Int64 TransactionId { get; set; }

        public String Message { get; set; }

        public Decimal Amount { get; set; }

        public String Type { get; set; }

        public Boolean IsCommited { get; set; }

        public TransactionViewModel(Transaction transaction)
        {
            TransactionId = transaction.TransactionId;
            Message = transaction.Message;
            IsCommited = transaction.IsCommited;
            Amount = transaction.Amount;
            Type = transaction.Type.ToString();
        }
    }
}