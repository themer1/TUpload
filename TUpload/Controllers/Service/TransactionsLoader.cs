using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TUpload.Models;
using TUpload.Models.DataLoader;

namespace TUpload.Controllers.Service
{
    public class TransactionsLoader
    {
        public List<Transaction> loadTransactions()
        {
            return TransactionsDBService.getTransactionsDbInstance().getExistingTransactions();
        }
    }
}