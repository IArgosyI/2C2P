using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.DAL.Client
{
    public class DalClient : IDalClient
    {
        public DalClient()
        {
            // This is a known issue for EF 6. We need this line to ensure that references are pulled correctly. More information at 
            // https://stackoverflow.com/questions/18455747/no-entity-framework-provider-found-for-the-ado-net-provider-with-invariant-name
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices); 
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");

        }

        public Transaction GetTransaction(string transactionId)
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

        public void UpdateTransaction(Transaction transaction)
        {
            using (DbEntities db = new DbEntities())
            {
                var transactionEntity = db.Transactions.SingleOrDefault(t => t.TransactionId == transaction.TransactionId);
                if (transactionEntity != null)
                {
                    // TODO decide whether to update existing record or throw exception or do nothin
                    // for now, replace the old entity
                    transactionEntity.Amount = transaction.Amount;
                    transactionEntity.CurrencyCode = transaction.CurrencyCode;
                    transactionEntity.TransactionDate = transaction.TransactionDate;
                    transactionEntity.Status = transaction.Status;
                }
                else
                {
                    db.Transactions.Add(transaction);
                }

                db.SaveChanges();
            }
        }
    }
}
