using ConsoleMoneyTracker.src.main.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public interface IRepository<T, U> where T : IIndexable<U>
    {
        IEnumerable<T> GetAll();
        T GetById(U objID);
        bool ContainsKey(U objID);
        void Insert(T obj);
        void Update(T obj);
        void Delete(U objID);
        int Count();
        void Save();
    }
}
