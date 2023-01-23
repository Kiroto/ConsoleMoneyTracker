using ConsoleMoneyTracker.src.main.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class InMemoryRepository<T, U> : IRepository<T, U> where T : IIndexable<U>
    {
        private IDictionary<U, T> data = new Dictionary<U, T>();

        public bool ContainsKey(U objID)
        {
            return data.ContainsKey(objID);
        }

        public int Count()
        {
            return data.Count;
        }

        public void Delete(U objID)
        {
            data.Remove(objID);
        }

        public IEnumerable<T> GetAll()
        {
            return data.Values;
        }

        public T GetById(U objID)
        {
            return data[objID];
        }

        public void Insert(T obj)
        {
            if (typeof(U) == typeof(int))
            {
                Type t = typeof(T);
                PropertyInfo pi = t.GetProperty("ID"); // Freaking reflection just to set this up.
                pi.SetValue(obj, data.Count());
            }
            data.Add(obj.ID, obj);
        }

        public void Save()
        {
            // NOOP for memory for now
        }

        public void Update(T obj)
        {
            data[obj.ID] = obj;
        }
    }
}
