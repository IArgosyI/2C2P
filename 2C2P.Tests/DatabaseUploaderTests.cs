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
    public class DatabaseUploaderTests
    {
        private DatabaseUploader _databaseUploader;

        private readonly Mock<IDalClient> _mockDalClient = new Mock<IDalClient>(MockBehavior.Strict);
        private readonly Mock<IFileParser<Transaction>> _mockFileParser = new Mock<IFileParser<Transaction>>(MockBehavior.Strict);
        private readonly Mock<ILogger> _mockLogger = new Mock<ILogger>();

        public DatabaseUploaderTests()
        {
            _databaseUploader = new DatabaseUploader(
                _mockDalClient.Object,
                _mockFileParser.Object,
                _mockLogger.Object
                );
        }

        [TestMethod]
        public void UploadTransactionsToDatabase_Success()
        {
            // Arrange
            var transactions = new List<Transaction>();
            _mockFileParser.Setup(m => m.Parse(It.IsAny<TextReader>(), ".xml"))
                .Returns(transactions);
            _mockDalClient.Setup(m => m.UpdateTransactions(transactions));

            // Act
            _databaseUploader.UploadTransactionsToDatabase((TextReader)null, ".xml");

            // Verify
        }

        [TestMethod]
        public void UploadTransactionsToDatabase_Parsing_Fail()
        {
            // Arrange
            _mockFileParser.Setup(m => m.Parse(It.IsAny<TextReader>(), ".xml"))
                .Returns((List<Transaction>)null);

            // Act
            _databaseUploader.UploadTransactionsToDatabase((TextReader)null, ".xml");

            // Verify
        }
    }
}
