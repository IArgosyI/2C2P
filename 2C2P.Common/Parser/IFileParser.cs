using _2C2P.DAL;
using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IFileParser<T>
    {
        /// <summary>
        /// Parse file to list of given file Type
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        List<Transaction> Parse(TextReader reader, string fileType);
    }
}