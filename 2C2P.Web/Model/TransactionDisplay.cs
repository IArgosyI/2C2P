using _2C2P.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2P.Web.Model
{
    /// <summary>
    /// Transaction object to be displayed
    /// </summary>
    public class TransactionDisplay
    {
        /// <summary>
        /// Transaction ID
        /// </summary>
        public string Id;

        /// <summary>
        /// Payment (Amount + CurrencyCode e.g. 300.00 USD)
        /// </summary>
        public string Payment;

        /// <summary>
        /// Transaction Status 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusForDisplay Status;

        public enum StatusForDisplay
        {
            A,R,D
        }

        // This is a mapping between STATUS value in Database and STATUS value to be displayed
        private static readonly Dictionary<string, StatusForDisplay> DISPLAY_STATUS_MAP = new Dictionary<string, StatusForDisplay>()
        {
            { "Approved", StatusForDisplay.A },
            { "Rejected", StatusForDisplay.R },
            { "Done", StatusForDisplay.D }
        };

        public static TransactionDisplay ConvertToDisplay(Transaction transaction)
        {
            if (transaction == null)
            {
                return null;
            }
            else
            {
                return new TransactionDisplay()
                {
                    Id = transaction.TransactionId,
                    Payment = $"{string.Format("{0:F2}", transaction.Amount)} {transaction.CurrencyCode}",
                    Status = DISPLAY_STATUS_MAP[transaction.Status],
                };
            }
        }
    }
}
