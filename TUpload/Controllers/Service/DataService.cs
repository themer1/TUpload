using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using TUpload.Models;
using TUpload.Models.DataLoader;
using System.Data;
using System.Linq;

namespace TUpload.Controllers.Service
{
    public class DataService
    {
        private const string VALID_TRANSACTIONS = "VALID_TRANSACTIONS";
        private const string INVALID_TRANSACTIONS = "INVALID_TRANSACTIONS";
        private TransactionsDBService dbService = TransactionsDBService.getTransactionsDbInstance();
        private List<ValidCurrency> validCurrencies = null;


        public Dictionary<String, LinkedList<Transaction>> parseData(String dataPath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(dataPath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            return loadData(xlRange);
        }
        private Boolean isTransactionValid(String currency)
        {
            if (validCurrencies == null)
            {
                validCurrencies = dbService.getValidCurrencies(); 
            }

            return validCurrencies.Where(p => p.Currency.Trim().Equals(currency)).ToList().Count > 0;
        }
        private Dictionary<String, LinkedList<Transaction>> loadData(Excel.Range xlRange)
        {
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            LinkedList<Transaction> validTransactions = new LinkedList<Transaction>();
            LinkedList<Transaction> invalidTransactions = new LinkedList<Transaction>();
            for (int i = 1; i <= rowCount; i++)
            {
                try
                {
                    string currency = xlRange.Cells[i, 1].Value2.ToString();
                    decimal amount = Decimal.Parse(xlRange.Cells[i, 2].Value2.ToString());
                    if (isTransactionValid(currency))
                    {
                        validTransactions.AddLast(new Transaction(currency, amount));
                    }
                    else invalidTransactions.AddLast(new Transaction(currency, amount));
                }
                catch(Exception ex)
                {
                    invalidTransactions.AddLast(new Transaction(xlRange.Cells[i, 1].Value2.ToString(), xlRange.Cells[i, 2].Value2.ToString()));
                }
            }

            return addParsedDataToDictionary(validTransactions, invalidTransactions);
        }

        private Dictionary<string, LinkedList<Transaction>> addParsedDataToDictionary(LinkedList<Transaction> validData, LinkedList<Transaction> invalidData)
        {
            Dictionary<string, LinkedList<Transaction>> parsedData = new Dictionary<string, LinkedList<Transaction>>(2);
            parsedData.Add(VALID_TRANSACTIONS, validData);
            parsedData.Add(INVALID_TRANSACTIONS, invalidData);
            return parsedData;
        }
        public void storeData(LinkedList<Transaction> data)
        {            
            dbService.persistTransactionsCollection(data);
        }
    }
}