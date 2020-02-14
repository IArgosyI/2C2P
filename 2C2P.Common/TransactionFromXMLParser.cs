﻿using _2C2P.DAL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2C2P.Common
{
    public class TransactionFromXMLParser : IParser<Transaction>
    {
        private readonly IDataValidator _dataValidator;
        private readonly ILogger _logger;


        public TransactionFromXMLParser(
            IDataValidator dataValidator,
            ILogger logger)
        {
            _dataValidator = dataValidator;
            _logger = logger;
        }

        public List<Transaction> Parse(TextReader reader)
        {
            var transactions = new List<Transaction>();

            XElement transactionsXE = XElement.Load(reader);
            foreach(var transactionXE in transactionsXE.Elements("Transaction"))
            {
                try
                {
                    var transactionId = transactionXE.Attribute("id").Value;

                    var paymentDetailsXE = transactionXE.Element("PaymentDetails");
                    var amount = double.Parse(paymentDetailsXE.Element("Amount").Value);
                    var currencyCode = paymentDetailsXE.Element("CurrencyCode").Value;
                    _dataValidator.Validate(DataType.CurrencyCode, currencyCode);

                    var transactionDate = DateTimeOffset.Parse(transactionXE.Element("TransactionDate").Value);

                    var status = transactionXE.Element("Status").Value;
                    _dataValidator.Validate(DataType.TransactionStatus, status);

                    transactions.Add(new Transaction
                    {
                        TransactionId = transactionId,
                        Amount = amount,
                        CurrencyCode = currencyCode,
                        TransactionDate = transactionDate,
                        Status = status
                    });
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException)
                    {
                        _logger.LogError($"{transactionXE.ToString()} cannot be parsed to Transaction object. Exception: {ex.Message}");
                    }
                }
            }

            return transactions;
        }

    }
}