using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _2C2P.Common;
using _2C2P.DAL;
using _2C2P.DAL.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2C2P.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IDalClient _dalClient;
        private readonly IParser<Transaction> _parser;
        private readonly ILogger _logger;

        public TransactionController(
            IDalClient dalClient,
            IParser<Transaction> parser,
            ILogger logger
            )
        {
            _dalClient = dalClient;
            _parser = parser;
            _logger = logger;
        }

        [HttpGet("transaction")]
        public Transaction GetTransaction(string transactionId)
        {
            return _dalClient.GetTransaction(transactionId);
        }

        [HttpPut("transaction")]
        public void AddTransaction(Transaction transaction)
        {
            _dalClient.UpdateTransaction(transaction);
        }

        [HttpPost("transaction")]
        public ActionResult<List<Transaction>> UploadTransactions(IFormFile file)
        {
            var filetype = Path.GetExtension(file.FileName).ToLower();
            if (filetype != ".xml" && filetype != ".csv")
            {
                return BadRequest($"File type {filetype} is not supported");
            }

            List<Transaction> transactions;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                transactions = _parser.Parse(reader);
            }

            return transactions;
        }
    }
}