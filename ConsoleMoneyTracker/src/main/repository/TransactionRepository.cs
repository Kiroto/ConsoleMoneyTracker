using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model.dbModel;
using ConsoleMoneyTracker.src.main.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class TransactionRepository : IRepository<Transaction, int>
    {
        private readonly SqliteDbContext _SqliteDbContext;
        public TransactionRepository()
        {
            _SqliteDbContext = new SqliteDbContext();
            _SqliteDbContext.Database.EnsureCreated();
        }
        public bool ContainsKey(int objID)
        {
            var exists = _SqliteDbContext.transactionDbs.Select(a => a.ID).Contains(objID);
            return exists;
        }

        public void Delete(int objID)
        {
            var item = new TransactionDb(GetById(objID));
            if (Object.ReferenceEquals(item, null))
            {
                Console.WriteLine("This account id doen't exists in the current context");
            }
            else
            {
                _SqliteDbContext.transactionDbs.Remove(item);
                Save();
                Console.WriteLine("This account was deleted");
            }
        }

        public IEnumerable<Transaction> GetAll()
        {
            IEnumerable<TransactionDb> transactionDb = _SqliteDbContext.transactionDbs.AsEnumerable();
            return transactionDb;
        }

        public Transaction GetById(int objID)
        {
            var transactionDb = _SqliteDbContext.transactionDbs.Where(a => a.ID.Equals(objID)).FirstOrDefault();
            if (transactionDb == null) return new TransactionDb(new Transaction());

            return transactionDb;
        }

        public void Insert(Transaction obj)
        {
            _SqliteDbContext.transactionDbs.Add(new TransactionDb(obj));
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(Transaction obj)
        {
            var oldTransaction = new TransactionDb(GetById(obj.ID));
            if (oldTransaction == null)
            {
                Console.WriteLine("This account id doen't exists in the current context");
                return;
            }
            var newTransaction = new TransactionDb(obj);
            oldTransaction.listItemId = newTransaction.listItemId;
            oldTransaction.categoryId = newTransaction.categoryId;
            oldTransaction.sourceAccountId = newTransaction.sourceAccountId;
            oldTransaction.targetAccountId = newTransaction.targetAccountId;
            _SqliteDbContext.transactionDbs.Update(oldTransaction);
            Save();
        }
        public int Count()
        {
            return GetAll().Count();
        }
    }
}
