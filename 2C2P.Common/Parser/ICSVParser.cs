using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface ICSVParser<T>
    {
        /// <summary>
        /// Parse CSV file to list of given file Type
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        List<T> Parse(TextReader reader);
    }
}