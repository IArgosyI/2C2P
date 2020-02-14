using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface ICSVParser<T>
    {
        List<T> Parse(TextReader reader);
    }
}