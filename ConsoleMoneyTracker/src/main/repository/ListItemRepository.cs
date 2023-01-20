using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class ListItemRepository : IRepository<ListItemDb, int>
    {
        private readonly SqliteDbContext _SqliteDbContext;
        public ListItemRepository()
        {
            _SqliteDbContext = new SqliteDbContext();
            _SqliteDbContext.Database.EnsureCreated();
        }
        public bool ContainsKey(int objID)
        {
            var exists = _SqliteDbContext.listItemDbs.Select(l => l.ID).Contains(objID);
            return exists;
        }

        public void Delete(int objID)
        {
            var item = GetById(objID); 
            if(Object.ReferenceEquals(item, null))
            {
                Console.WriteLine("This listitem id doen't exists in the current context");
            }
            else
            {
                _SqliteDbContext.listItemDbs.Remove(item);
                Save();
                Console.WriteLine("This listitem was deleted");
            }
        }

        public IEnumerable<ListItemDb> GetAll()
        {
            IEnumerable<ListItemDb> listItemDb = _SqliteDbContext.listItemDbs.AsEnumerable();
            return listItemDb;
        }

        public ListItemDb GetById(int objID)
        {
            var itemDb = _SqliteDbContext.listItemDbs.Where(l => l.ID.Equals(objID)).FirstOrDefault();
            if (itemDb == null) return new ListItemDb();
            
            return itemDb;
        }

        public void Insert(ListItemDb obj)
        {
            _SqliteDbContext.listItemDbs.Add(obj);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(ListItemDb obj)
        {
            var item = GetById(obj.ID);
            if (item == null)
            {
                Console.WriteLine("This listitem id doen't exists in the current context");
                return;
            }
            item.name = obj.name;
            item.description = obj.description;
            item.shortName = obj.shortName;
            item.foregroundColor = obj.foregroundColor;
            item.backgroundColor = obj.backgroundColor;
            item.creationDate = obj.creationDate;
            item.removalDate = obj.removalDate;
            _SqliteDbContext.listItemDbs.Update(item);
            Save();

        }
        public int Count()
        {
            return 0;
        }
    }
}
