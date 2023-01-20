using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model.dbModel;
using SQLite;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class AccountRepository : IRepository<AccountDb, int>
    {
        private readonly SqliteDbContext _SqliteDbContext;
        public AccountRepository()
        {
            _SqliteDbContext = new SqliteDbContext();
            _SqliteDbContext.Database.EnsureCreated();
        }
        public bool ContainsKey(int objID)
        {
            var exists = _SqliteDbContext.accountDbs.Select(a => a.ID).Contains(objID);
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
                _SqliteDbContext.accountDbs.Remove(item);
                Save();
                Console.WriteLine("This account was deleted");
            }
        }

        public IEnumerable<AccountDb> GetAll()
        {
            IEnumerable<AccountDb> accountDb = _SqliteDbContext.accountDbs.AsEnumerable();
            return accountDb;
        }

        public AccountDb GetById(int objID)
        {
            var accountDb = _SqliteDbContext.accountDbs.Where(a => a.ID.Equals(objID)).FirstOrDefault();
            if (accountDb == null) return new AccountDb();

            return accountDb;
        }

        public void Insert(AccountDb obj)
        {
            _SqliteDbContext.accountDbs.Add(obj);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(AccountDb obj)
        {
            var account = GetById(obj.ID);
            if (account == null)
            {
                Console.WriteLine("This account id doen't exists in the current context");
                return;
            }
            account.listItemId = obj.listItemId;
            account.currencyId = obj.currencyId;
            account.amount = obj.amount;
            _SqliteDbContext.accountDbs.Update(account);
            Save();
        }
        public int Count()
        {
            return 0;
        }
    }
}
