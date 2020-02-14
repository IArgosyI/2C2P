using System.Collections.Generic;
using System.IO;

namespace _2C2P.Common
{
    public interface IParser<T>
    {
        // Parse given string to List of object
        List<T> Parse(TextReader reader);
    }
}