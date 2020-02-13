using _2C2P.DAL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2C2P.Common
{
    public class CSVParser<T> : IParser<T>
    {
        private readonly ILogger _logger;


        private const int TRANSACTION_COL_COUNT = 5;
        private List<string> TRANSACTION_STATUS_ALLOW = new List<string>() { "Approved", "Failed", "Finished" };

        public CSVParser(
            ILogger logger)
        {
            _logger = logger;
        }

        public List<T> Parse(string s)
        {
            Type itemType = typeof(T);
            if (itemType == typeof(Transaction)) 
            {
                return (List<T>)Convert.ChangeType(ParseTransaction(s), typeof(List<T>));
            }
            else
            {
                _logger.LogError($"Error: CSV parser of type {typeof(T)} is not implemented.");
                throw new NotImplementedException();
            }
        }

        private List<Transaction> ParseTransaction(string s)
        {
            var transactions = new List<Transaction>();
            using (TextReader reader = new StringReader(s))
            {
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
                        try
                        {
                            transactions.Add(new Transaction
                            {
                                TransactionId = fields[0],
                                Amount = double.Parse(fields[1]),
                                CurrencyCode = ValidateItem(ValidateType.CurrencyCode, fields[2]),
                                // TODO: Confirm the date time format
                                TransactionDate = DateTimeOffset.ParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", null),
                                Status = ValidateItem(ValidateType.TransactionStatus, fields[4])
                            });
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Parsing failed with exception {ex.Message}");
                        }
                    }
                }
            }
            return transactions;
        }

        private enum ValidateType 
        {
            CurrencyCode,
            TransactionStatus
        }

        private string ValidateItem(ValidateType type, string value)
        {
            if (type == ValidateType.CurrencyCode)
            {
                if (value.Length > 3) throw new InvalidDataException($"Currency code need to be 3 characters long: {value}");
            }
            else if (type == ValidateType.TransactionStatus) 
            {
                if (!TRANSACTION_STATUS_ALLOW.Contains(value)) throw new InvalidDataException($"Status {value} is not allowed"); 
            }

            return value;
        }


    }
}
