using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model.dbModel;
using ConsoleMoneyTracker.src.main.model;
using SQLite;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class AccountRepository : IRepository<Account, int>
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
            var account = new AccountDb(GetById(objID));
            if (Object.ReferenceEquals(account, null))
            {
                Console.WriteLine("This account id doen't exists in the current context");
            }
            else
            {
                _SqliteDbContext.accountDbs.Remove(account);
                Save();
                Console.WriteLine("This account was deleted");
            }
        }

        public IEnumerable<Account> GetAll()
        {
            IEnumerable<AccountDb> accountDb = _SqliteDbContext.accountDbs.AsEnumerable();
            return accountDb;
        }

        public Account GetById(int objID)
        {
            var accountDb = _SqliteDbContext.accountDbs.Where(a => a.ID.Equals(objID)).FirstOrDefault();
            if (accountDb == null) return new AccountDb(new Account());

            return accountDb;
        }

        public void Insert(Account obj)
        {
            AccountDb accountDb = new AccountDb(obj);
            _SqliteDbContext.accountDbs.Add(accountDb);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(Account obj)
        {

            var oldAccount = new AccountDb(GetById(obj.ID));
            if (oldAccount == null)
            {
                Console.WriteLine("This account id doen't exists in the current context");
                return;
            }
            AccountDb accountDb = new AccountDb(obj);
            oldAccount.listItemId = accountDb.listItemId;
            oldAccount.currencyId = accountDb.currencyId;
            oldAccount.amount = obj.amount;
            _SqliteDbContext.accountDbs.Update(oldAccount);
            Save();
        }
        public int Count()
        {
            return GetAll().Count();
        }
    }
}
