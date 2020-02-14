using _2C2P.DAL;
using _2C2P.DAL.Client;
using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public class DatabaseUploader : IDatabaseUploader
    {
        private readonly IDalClient _dalClient;
        private readonly IFileParser<Transaction> _transactionParser;
        private readonly ILogger _logger;

        public DatabaseUploader(
            IDalClient dalClient, 
            IFileParser<Transaction> transactionParser,
            ILogger logger)
        {
            _dalClient = dalClient;
            _transactionParser = transactionParser;
            _logger = logger;
        }

        public List<Transaction> UploadTransactionsToDatabase(TextReader reader, string fileType)
        {
            var transactions = _transactionParser.Parse(reader, fileType);

            // if transaction is null, then something went wrong with the parser, skip uploading
            if (transactions != null)
            {
                _logger.LogInformation($"Uploading {transactions.Count} transactions to Database");
                _logger.LogMetric("TransactionUpload", transactions.Count);
                _dalClient.UpdateTransactions(transactions);
            }

            return transactions;
        }
    }
}
