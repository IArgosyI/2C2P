using System.Collections.Generic;

namespace _2C2P.Common
{
    public interface IParser<T>
    {
        // Parse given string to List of object
        List<T> Parse(string s);
    }
}