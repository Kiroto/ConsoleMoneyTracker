using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int objID);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int objID);
        void Save();
    }
}
