using System;
using System.Collections.Generic;

namespace _2C2P.DAL.Client
{
    public interface IDalClient
    {
        Transaction GetTransaction(string transactionId);

        List<Transaction> GetTransactionByCurrency(string currencyCode);

        List<Transaction> GetTransactionsByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);

        List<Transaction> GetTransactionsByStatus(string status);

        void UpdateTransaction(Transaction transaction);

        void UpdateTransactions(List<Transaction> transactions);
    }
}