using _2C2P.DAL;
using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IDatabaseUploader
    {
        /// <summary>
        /// From file, parse it and upload the transactions to the Database
        /// If file contains more than one object, all the object need to be correct format in order
        /// to get uploaded. If one object fail, the whole request will not get uploaded
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        List<Transaction> UploadTransactionsToDatabase(TextReader reader, string fileType);
    }
}