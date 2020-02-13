using System;
using System.Collections.Generic;

namespace _2C2P.DAL.Client
{
    public interface IDalClient
    {
        Transaction GetTransaction(string transactionId);

        List<Transaction> GetTransactionByCurrency(string currencyCode);

        List<Transaction> GetTransactionByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);

        List<Transaction> GetTransactionByStatus(string status);

        void UpdateTransaction(Transaction transaction);
    }
}