using _2C2P.DAL;
using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IFileParser<T>
    {
        List<Transaction> Parse(TextReader reader, string fileType);
    }
}