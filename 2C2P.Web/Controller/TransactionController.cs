using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _2C2P.Common;
using _2C2P.DAL;
using _2C2P.DAL.Client;
using _2C2P.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2C2P.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IDalClient _dalClient;
        private readonly IDatabaseUploader _databaseUploader;
        private readonly ILogger _logger;

        private const long MEGABYTE = 1024 * 1024;

        public TransactionController(
            IDalClient dalClient,
            IDatabaseUploader databaseUploader,
            ILogger logger
            )
        {
            _dalClient = dalClient;
            _databaseUploader = databaseUploader;
            _logger = logger;
        }

        /// <summary>
        /// Get Transaction by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet("transaction")]
        public TransactionDisplay GetTransaction(string transactionId)
        {
            // Assumption : Transaction Id is unique and only return one record at most
            return TransactionDisplay.ConvertToDisplay(_dalClient.GetTransaction(transactionId));
        }

        /// <summary>
        /// Get Transaction by start and end date
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet("transaction/dateRange")]
        public List<TransactionDisplay> GetTransactionsByDateRange(DateTimeOffset start, DateTimeOffset end)
        {
            return _dalClient.GetTransactionsByDateRange(start, end)
                .Select(t => TransactionDisplay.ConvertToDisplay(t)).ToList();
        }

        /// <summary>
        /// Get transaction by status. Status need to be either "Approved", "Rejected" or "Done"
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("transaction/status")]
        public List<TransactionDisplay> GetTransactionsByStatus(string status)
        {
            return _dalClient.GetTransactionsByStatus(status)
                .Select(t => TransactionDisplay.ConvertToDisplay(t)).ToList();
        }

        /// <summary>
        /// Update transaction to be as the given input. If no transaction exist, it will create a new one
        /// </summary>
        /// <param name="transaction"></param>
        [HttpPut("transaction")]
        public void UpdateTransaction(Transaction transaction)
        {
            _dalClient.UpdateTransaction(transaction);
        }

        /// <summary>
        /// Upload transaction from either CSV or XML file
        /// </summary>
        /// <param name="file"></param>
        /// <returns> List of Transactions that is uploaded to Database </returns>
        [HttpPost("transaction")]
        public ActionResult<List<Transaction>> UploadTransactions(IFormFile file)
        {
            var filetype = Path.GetExtension(file.FileName).ToLower();
            if (filetype != ".xml" && filetype != ".csv")
            {
                return BadRequest($"File type {filetype} is not supported");
            }
            if (file.Length > MEGABYTE)
            {
                return BadRequest($"File {file.FileName} is too large. Please upload file that is less than 1 MB");
            }

            List<Transaction> transactions = new List<Transaction>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                transactions = _databaseUploader.UploadTransactionsToDatabase(reader, filetype);
            }

            if (transactions == null)
            {
                return BadRequest($"File {file.FileName} contains un-parsable items, please check the file/log for more information");
            }

            return transactions;
        }
    }
}