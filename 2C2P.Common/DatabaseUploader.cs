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

        public DatabaseUploader(
            IDalClient dalClient, 
            IFileParser<Transaction> transactionParser)
        {
            _dalClient = dalClient;
            _transactionParser = transactionParser;
        }

        public List<Transaction> UploadTransactionsToDatabase(TextReader reader, string fileType)
        {
            var transactions = _transactionParser.Parse(reader, fileType);

            _dalClient.UpdateTransactions(transactions);

            return transactions;
        }
    }
}
