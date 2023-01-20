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
        public bool ContainsKey(int objID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int objID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Transaction GetById(int objID)
        {
            throw new NotImplementedException();
        }

        public void Insert(Transaction obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}
