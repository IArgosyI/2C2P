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

        public DatabaseUploaderTests()
        {
            _databaseUploader = new DatabaseUploader(
                _mockDalClient.Object,
                _mockFileParser.Object
                );
        }

        [TestMethod]
        public void UploadTransactionsToDatabase_Test()
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
    }
}
