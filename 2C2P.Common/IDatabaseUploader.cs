using _2C2P.DAL;
using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IDatabaseUploader
    {
        List<Transaction> UploadTransactionsToDatabase(TextReader reader, string fileType);
    }
}