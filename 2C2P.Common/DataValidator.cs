using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.Common
{
    public class DataValidator: IDataValidator
    {
        private readonly List<string> TRANSACTION_STATUS_ALLOW = new List<string>() { "Approved", "Rejected", "Done" };

        public DataValidator()
        {

        }

        public bool Validate(DataType type, string value)
        {
            if (type == DataType.CurrencyCode)
            {
                if (value.Length > 3) throw new FormatException($"Currency code need to be 3 characters long: {value}");
            }
            else if (type == DataType.TransactionStatus)
            {
                if (!TRANSACTION_STATUS_ALLOW.Contains(value)) throw new FormatException($"Status {value} is not allowed");
            }

            return true;
        }
    }
}