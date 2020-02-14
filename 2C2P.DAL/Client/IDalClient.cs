using System;
using System.Collections.Generic;

namespace _2C2P.DAL.Client
{
    public interface IDalClient
    {
        /// <summary>
        /// Get Transaction by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Transaction GetTransaction(string transactionId);

        /// <summary>
        /// Get Transaction by Currency
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        List<Transaction> GetTransactionByCurrency(string currencyCode);

        /// <summary>
        /// Get Transaction by given start and end date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<Transaction> GetTransactionsByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Get transaction by status. Status need to be either "Approved", "Rejected" or "Done"
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        List<Transaction> GetTransactionsByStatus(string status);

        /// <summary>
        /// Update transaction to be as the given input. If no transaction exist, it will create a new one
        /// </summary>
        /// <param name="transaction"></param>
        void UpdateTransaction(Transaction transaction);

        /// <summary>
        /// Update list of transactions to be as the given input. If no transaction exist, it will create a new one
        /// </summary>
        /// <param name="transactions"></param>
        void UpdateTransactions(List<Transaction> transactions);
    }
}