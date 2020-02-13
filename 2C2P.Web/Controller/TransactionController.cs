using System;
using System.Collections.Generic;
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
        private readonly ILogger _logger;

        public TransactionController(
            IDalClient dalClient,
            ILogger logger
            )
        {
            _dalClient = dalClient;
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
    }
}