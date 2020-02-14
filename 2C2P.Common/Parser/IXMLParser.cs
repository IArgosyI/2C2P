using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IXMLParser<T>
    {
        List<T> Parse(TextReader reader);
    }
}