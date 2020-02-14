using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IParser<T>
    {
        /// <summary>
        /// Parse given file into List of Enity objects.
        /// This will skip any invalid object in the list
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        List<T> Parse(TextReader reader);
    }
}