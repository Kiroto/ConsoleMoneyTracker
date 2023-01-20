using ConsoleMoneyTracker.src.main.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class ConfigurationRepository : IRepository<Configuration, string>
    {
        public bool ContainsKey(string objID)
        {
            throw new NotImplementedException();
        }

        public void Delete(string objID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Configuration> GetAll()
        {
            throw new NotImplementedException();
        }

        public Configuration GetById(string objID)
        {
            throw new NotImplementedException();
        }

        public void Insert(Configuration obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Configuration obj)
        {
            throw new NotImplementedException();
        }
        public int Count()
        {
            return 0;
        }
    }
}
