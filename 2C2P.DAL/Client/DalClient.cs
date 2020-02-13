using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.DAL.Client
{
    public class DalClient
    {
        public DalClient()
        {
        }

        public Transaction GetTransaction(int transactionId)
        {
            using (DbEntities db = new DbEntities())
            {
                return db.Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();
            }
        }

        public List<Transaction> GetTransactionByCurrency(string currencyCode)
        {
            using (DbEntities db = new DbEntities())
            {
                return db.Transactions.Where(t => t.CurrencyCode == currencyCode).ToList();
            }
        }

        public List<Transaction> GetTransactionByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            using (DbEntities db = new DbEntities())
            {
                return db.Transactions.Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate).ToList();
            }
        }

        public List<Transaction> GetTransactionByStatus(string status)
        {
            using (DbEntities db = new DbEntities())
            {
                return db.Transactions.Where(t => t.Status == status).ToList();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            using (DbEntities db = new DbEntities())
            {
                var transactionEntity = db.Transactions.SingleOrDefault(t => t.TransactionId == transaction.TransactionId);
                if (transactionEntity != null)
                {
                    // TODO decide whether to update existing record or throw exception or do nothin
                    // for now, do nothing
                }
                else
                {
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                }
            }
        }
    }
}
