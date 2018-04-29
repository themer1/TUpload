using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TUpload.Models.DataLoader
{
    public class TransactionsDBService
    {
        private static TransactionsDBService transactionInstance;
        private TransactionsDBService()
        {
            
        }

        public static TransactionsDBService getTransactionsDbInstance()
        {
            if (transactionInstance == null)
            {
                transactionInstance = new TransactionsDBService();
            }
            return transactionInstance;
        }
        public void persistTransaction(Transaction transaction)
        {
            using (TransactionsDBEntities1 entities = new TransactionsDBEntities1())
            {                
                entities.Transactions.Add(transaction);
                entities.SaveChanges();
            }
        }

        public void persistTransactionsCollection(LinkedList<Transaction> transactions)
        {
            using (TransactionsDBEntities1 entities = new TransactionsDBEntities1())
            {
                entities.Transactions.AddRange(transactions);
                entities.SaveChanges();
            }
        }

        public List<Transaction> getExistingTransactions()
        {
            using (TransactionsDBEntities1 entities = new TransactionsDBEntities1())
            {
                var transactions = from r in entities.Transactions select r;
                return transactions.ToList();
            }
        }

        public List<ValidCurrency> getValidCurrencies()
        {
            using (TransactionsDBEntities1 entities = new TransactionsDBEntities1())
            {
                var validCurrencies = from r in entities.ValidCurrencies select r;
                return validCurrencies.ToList();
            }
        }
                
    }
}