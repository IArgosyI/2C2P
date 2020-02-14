using _2C2P.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.Common
{
    public class TransactionFileParser: IFileParser<Transaction>
    {
        private readonly IXMLParser<Transaction> _xMLParser;
        private readonly ICSVParser<Transaction> _cSVParser;
        public TransactionFileParser(
            IXMLParser<Transaction> xMLParser,
            ICSVParser<Transaction> cSVParser)
        {
            _xMLParser = xMLParser;
            _cSVParser = cSVParser;
        }
        public List<Transaction> Parse(TextReader reader, string fileType)
        {
            if (fileType == ".xml")
            {
                return _xMLParser.Parse(reader);
            }
            else if(fileType == ".csv")
            {
                return _cSVParser.Parse(reader);
            }
            else
            {
                throw new NotImplementedException($"File type {fileType} parser is not implemented");
            }
        }
    }
}
