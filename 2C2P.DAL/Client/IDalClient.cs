using System;
using System.Collections.Generic;

namespace _2C2P.DAL.Client
{
    public interface IDalClient
    {
        Transaction GetTransaction(int transactionId);

        List<Transaction> GetTransactionByCurrency(string currencyCode);

        List<Transaction> GetTransactionByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);

        List<Transaction> GetTransactionByStatus(string status);

        void AddTransaction(Transaction transaction);
}