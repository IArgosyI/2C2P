using _2C2P.Common;
using _2C2P.DAL;
using _2C2P.DAL.Client;
using _2C2P.Web.Controller;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2C2P.Tests
{
    [TestClass]
    public class TransactionControllerTests
    {
        private TransactionController _transactionController;

        private readonly Mock<IDalClient> _mockDalClient = new Mock<IDalClient>(MockBehavior.Strict);
        private readonly Mock<IDatabaseUploader> _mockDatabaseUploader = new Mock<IDatabaseUploader>(MockBehavior.Strict);
        private readonly Mock<ILogger> _mockLogger = new Mock<ILogger>();

        private const string TRANSACTION_ID = "Transaction Id";

        public TransactionControllerTests()
        {
            _transactionController = new TransactionController(
                _mockDalClient.Object,
                _mockDatabaseUploader.Object,
                _mockLogger.Object
                );
        }

        [TestMethod]
        public void GetTransaction_Test()
        {
            // Arrange
            _mockDalClient.Setup(d => d.GetTransaction(TRANSACTION_ID))
                .Returns((Transaction)null);

            // Act
            var result = _transactionController.GetTransaction(TRANSACTION_ID);

            // Verify
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetTransactionsByDateRange_Test()
        {
            // Arrange
            var startDate = new DateTimeOffset(1, 1, 1, 1, 1, 1, new TimeSpan());
            var endDate = new DateTimeOffset(1, 2, 3, 4, 5, 6, new TimeSpan());

            _mockDalClient.Setup(d => d.GetTransactionsByDateRange(startDate, endDate))
                .Returns(new List<Transaction>());

            // Act
            var result = _transactionController.GetTransactionsByDateRange(startDate, endDate);

            // Verify
            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetTransactionsByStatus_Test()
        {
            // Arrange
            string status = "STATUS";
            _mockDalClient.Setup(d => d.GetTransactionsByStatus(status))
                .Returns(new List<Transaction>());

            // Act
            var result = _transactionController.GetTransactionsByStatus(status);

            // Verify
            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void UpdateTransaction_Test()
        {
            // Arrange
            _mockDalClient.Setup(d => d.UpdateTransaction(It.IsAny<Transaction>()));

            // Act
            _transactionController.UpdateTransaction(new Transaction());

            // Verify
        }

        [TestMethod]
        public void UploadTransactions_WrongFileType()
        {
            // Arrange
            var file = new FormFile(null, 0, 1, "", "file.1");

            // Act
            var result = _transactionController.UploadTransactions(file).Result as BadRequestObjectResult;

            // Verify
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.ToString().Should().Be($"File type .1 is not supported");
        }

        [TestMethod]
        public void UploadTransactions_FileTooBig()
        {
            // Arrange
            var file = new FormFile(null, 0, 1024 * 1024 + 1, "", "file.xml");

            // Act
            var result = _transactionController.UploadTransactions(file).Result as BadRequestObjectResult;

            // Verify
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.ToString().Should().Be($"File file.xml is too large. Please upload file that is less than 1 MB");
        }

        [TestMethod]
        public void UploadTransactions_Success()
        {
            // Arrange
            string test = "Test";
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            MemoryStream stream = new MemoryStream(byteArray);
            var file = new FormFile(stream, 0, 1, "", "file.xml");
            _mockDatabaseUploader.Setup(m => m.UploadTransactionsToDatabase(It.IsAny<TextReader>(), ".xml"))
                .Returns(new List<Transaction>());

            // Act
            var result = _transactionController.UploadTransactions(file).Result;

            // Verify
        }
    }
}
