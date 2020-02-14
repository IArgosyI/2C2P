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
        private readonly List<string> TRANSACTION_STATUS_ALLOW = new List<string>() { "Approved", "Failed", "Finished" };

        public DataValidator()
        {

        }

        public bool Validate(DataType type, string value)
        {
            if (type == DataType.CurrencyCode)
            {
                if (value.Length > 3) return false;
            }
            else if (type == DataType.TransactionStatus)
            {
                if (!TRANSACTION_STATUS_ALLOW.Contains(value)) return false;
            }

            return true;
        }
    }
}
