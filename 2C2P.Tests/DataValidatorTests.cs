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
    public class DataValidatorTests
    {
        private DataValidator _dataValidator;

        public DataValidatorTests()
        {
            _dataValidator = new DataValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void UploadTransactionsToDatabase_Fail_With_4_Letters_Currency_Code()
        {
            // Arrange

            // Act
            _dataValidator.Validate(DataType.CurrencyCode, "ABCD");

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void UploadTransactionsToDatabase_Fail_With_2_Letters_Currency_Code()
        {
            // Arrange

            // Act
            _dataValidator.Validate(DataType.CurrencyCode, "AB");

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void UploadTransactionsToDatabase_Fail_Status_Not_Allowed()
        {
            // Arrange

            // Act
            _dataValidator.Validate(DataType.TransactionStatus, "ABC");

            // Verify
        }

        [TestMethod]
        public void UploadTransactionsToDatabase_Success()
        {
            // Arrange

            // Act
            _dataValidator.Validate(DataType.CurrencyCode, "ABC");

            _dataValidator.Validate(DataType.TransactionStatus, "Approved");
            _dataValidator.Validate(DataType.TransactionStatus, "Rejected");
            _dataValidator.Validate(DataType.TransactionStatus, "Done");

            // Verify
        }
    }
}
