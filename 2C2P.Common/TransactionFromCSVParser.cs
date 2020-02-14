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
    public class TransactionFromCSVParser : IParser<Transaction>
    {
        private readonly IDataValidator _dataValidator;
        private readonly ILogger _logger;


        private const int TRANSACTION_COL_COUNT = 5;

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

                        // Design : Decide whether to fail everything, or just skip this one
                        continue;
                    }

                    if (!_dataValidator.Validate(DataType.CurrencyCode, fields[2]) ||
                        !double.TryParse(fields[1], out double amount) ||
                        // TODO: Confirm the date time format
                        !DateTimeOffset.TryParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out DateTimeOffset transactionDate) ||
                        !_dataValidator.Validate(DataType.TransactionStatus, fields[4]))
                    {
                        _logger.LogError($"{string.Join(",", fields)} cannot be parsed to Transaction object");
                        continue;
                    }

                    transactions.Add(new Transaction
                    {
                        TransactionId = fields[0],
                        Amount = amount,
                        CurrencyCode = fields[2],
                        TransactionDate = transactionDate,
                        Status = fields[4]
                    });
                }
            }
            return transactions;
        }

    }
}
