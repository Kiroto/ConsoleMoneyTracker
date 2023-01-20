using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class CurrencyRepository : IRepository<CurrencyDb, string>
    {
        private readonly SqliteDbContext _SqliteDbContext;
        public CurrencyRepository()
        {
            _SqliteDbContext = new SqliteDbContext();
            _SqliteDbContext.Database.EnsureCreated();
        }
        public bool ContainsKey(string objID)
        {
            var exists = _SqliteDbContext.currerncyDbs.Select(c => c.ID).Contains(objID);
            return exists;
        }

        public void Delete(string objID)
        {
            var item = GetById(objID);
            if (Object.ReferenceEquals(item, null))
            {
                Console.WriteLine("This currency id doen't exists in the current context");
            }
            else
            {
                _SqliteDbContext.currerncyDbs.Remove(item);
                Save();
                Console.WriteLine("This currency was deleted");
            }
        }

        public IEnumerable<CurrencyDb> GetAll()
        {
            IEnumerable<CurrencyDb> currencyDb = _SqliteDbContext.currerncyDbs.AsEnumerable();
            return currencyDb;
        }

        public CurrencyDb GetById(string objID)
        {
            var currencyDb = _SqliteDbContext.currerncyDbs.Where(c => c.ID.Equals(objID)).FirstOrDefault();
            if (currencyDb == null) return new CurrencyDb();

            return currencyDb;
        }

        public void Insert(CurrencyDb obj)
        {
            _SqliteDbContext.currerncyDbs.Add(obj);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(CurrencyDb obj)
        {
            var currency = GetById(obj.ID);
            if (currency == null)
            {
                Console.WriteLine("This currency id doen't exists in the current context");
                return;
            }
            currency.listItemId = obj.listItemId;
            currency.apiIdentifier = obj.apiIdentifier;
            currency.lastUpdated = obj.lastUpdated;
            currency.toDollar = obj.toDollar;   
            _SqliteDbContext.currerncyDbs.Update(currency);
            Save();
        }
        public int Count()
        {
            return 0;
        }
    }
}
