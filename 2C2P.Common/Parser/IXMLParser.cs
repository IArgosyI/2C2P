using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IXMLParser<T>
    {
        /// <summary>
        /// Parse XML file to list of given file Type
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        List<T> Parse(TextReader reader);
    }
}