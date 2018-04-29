using Microsoft.VisualStudio.TestTools.UnitTesting;
using TUpload.Controllers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUpload.Models;

namespace TUpload.Controllers.Service.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        DataService dataServiceToTest = new DataService();
        private const string VALID_TRANSACTIONS = "VALID_TRANSACTIONS";
        private const string INVALID_TRANSACTIONS = "INVALID_TRANSACTIONS";
        private const string TEMP_DATA = "TempData";

        [TestMethod()]
        public void parseDataTest_transactions_1_invalid_amount()
        {
            string transactions_1_invalid_amount_location = @"C:\Users\UMER\Documents\Visual Studio 2017\Projects\TUpload\TUpload\TempData\transactions_1_invalid_amount.xlsx";
            Dictionary<string, LinkedList<Transaction>> parserResult = dataServiceToTest.parseData(transactions_1_invalid_amount_location);
            Assert.AreEqual(parserResult[VALID_TRANSACTIONS].Count(), 0);
            Assert.AreEqual(parserResult[INVALID_TRANSACTIONS].Count(), 1);            
        }

        [TestMethod()]
        public void parseDataTest_transactions_1_invalid_currency()
        {
            string transactions_1_invalid_amount_location = @"C:\Users\UMER\Documents\Visual Studio 2017\Projects\TUpload\TUpload\TempData\transactions_1_invalid_currency.xlsx";
            Dictionary<string, LinkedList<Transaction>> parserResult = dataServiceToTest.parseData(transactions_1_invalid_amount_location);
            Assert.AreEqual(parserResult[VALID_TRANSACTIONS].Count(), 0);
            Assert.AreEqual(parserResult[INVALID_TRANSACTIONS].Count(), 1);
        }

        [TestMethod()]
        public void parseDataTest_transactions_1_invalid_lower_case()
        {
            string transactions_1_invalid_amount_location = @"C:\Users\UMER\Documents\Visual Studio 2017\Projects\TUpload\TUpload\TempData\transactions_1_invalid_lower_case.xlsx";
            Dictionary<string, LinkedList<Transaction>> parserResult = dataServiceToTest.parseData(transactions_1_invalid_amount_location);
            Assert.AreEqual(parserResult[VALID_TRANSACTIONS].Count(), 0);
            Assert.AreEqual(parserResult[INVALID_TRANSACTIONS].Count(), 1);
        }

        [TestMethod()]
        public void parseDataTest_transactions_1_valid_record()
        {
            string transactions_1_invalid_amount_location = @"C:\Users\UMER\Documents\Visual Studio 2017\Projects\TUpload\TUpload\TempData\transactions_1_valid_record.xlsx";
            Dictionary<string, LinkedList<Transaction>> parserResult = dataServiceToTest.parseData(transactions_1_invalid_amount_location);
            Assert.AreEqual(parserResult[VALID_TRANSACTIONS].Count(), 0);
            Assert.AreEqual(parserResult[INVALID_TRANSACTIONS].Count(), 1);
        }

    }
}