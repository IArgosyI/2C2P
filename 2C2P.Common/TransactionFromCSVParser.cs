using _2C2P.DAL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2C2P.Common
{
    public class TransactionFromCSVParser : ICSVParser<Transaction>
    {
        private readonly IDataValidator _dataValidator;
        private readonly ILogger _logger;


        private const int TRANSACTION_COL_COUNT = 5;
        // This is a mapping between STATUS value in CSV file and STATUS value in Database
        private readonly Dictionary<string, string> CSV_STATUS_MAP = new Dictionary<string, string>()
        {
            { "Approved", "Approved" },
            { "Failed", "Rejected" },
            { "Finished", "Done" }
        };

        public TransactionFromCSVParser(
            IDataValidator dataValidator,
            ILogger logger)
        {
            _dataValidator = dataValidator;
            _logger = logger;
        }

        public List<Transaction> Parse(TextReader reader)
        {
            var transactions = new List<Transaction>();
            using (TextFieldParser parser = new TextFieldParser(reader))
            {
                parser.SetDelimiters(new string[] { "," });
                parser.HasFieldsEnclosedInQuotes = true;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Count() != TRANSACTION_COL_COUNT)
                    {
                        _logger.LogError($"{string.Join(",", fields)} doesn't contain all required columns");

                        continue;
                    }

                    try
                    {
                        var transactionId = fields[0];
                        var amount = double.Parse(fields[1]);
                        var currencyCode = _dataValidator.Validate(DataType.TransactionStatus, fields[4]) ? fields[4] : null;
                        var transactionDate = DateTimeOffset.ParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", null);
                        var status = CSV_STATUS_MAP[fields[4]];

                        transactions.Add(new Transaction
                        {
                            TransactionId = transactionId,
                            Amount = amount,
                            CurrencyCode = currencyCode,
                            TransactionDate = transactionDate,
                            Status = status
                        });
                    }
                    catch(Exception ex)
                    {
                        if (ex is FormatException || ex is KeyNotFoundException)
                        {
                            _logger.LogError($"{string.Join(",", fields)} cannot be parsed to Transaction object. Exception: {ex.Message}");
                        }
                    }
                }
            }
            return transactions;
        }

    }
}
