using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class TransactionRepository : IRepository<TransactionDb, int>
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
            var item = GetById(objID);
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

        public IEnumerable<TransactionDb> GetAll()
        {
            IEnumerable<TransactionDb> transactionDb = _SqliteDbContext.transactionDbs.AsEnumerable();
            return transactionDb;
        }

        public TransactionDb GetById(int objID)
        {
            var transactionDb = _SqliteDbContext.transactionDbs.Where(a => a.ID.Equals(objID)).FirstOrDefault();
            if (transactionDb == null) return new TransactionDb();

            return transactionDb;
        }

        public void Insert(TransactionDb obj)
        {
            _SqliteDbContext.transactionDbs.Add(obj);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(TransactionDb obj)
        {
            var transaction = GetById(obj.ID);
            if (transaction == null)
            {
                Console.WriteLine("This account id doen't exists in the current context");
                return;
            }
            transaction.listItemId = obj.listItemId;
            transaction.categoryId = obj.categoryId;
            transaction.sourceAccountId = obj.sourceAccountId;
            transaction.targetAccountId = obj.targetAccountId;
            _SqliteDbContext.transactionDbs.Update(transaction);
            Save();
        }
        public int Count()
        {
            return GetAll().Count();
        }
    }
}
