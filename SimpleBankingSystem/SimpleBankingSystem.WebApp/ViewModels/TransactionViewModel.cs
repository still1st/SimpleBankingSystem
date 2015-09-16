using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.WebApp.ViewModels
{
    /// <summary>
    /// ViewModel for result of transaction executing
    /// </summary>
    public class TransactionViewModel
    {
        public Decimal Balance { get; set; }

        public Transaction Transaction { get; set; }
    }
}