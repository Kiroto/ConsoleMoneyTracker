﻿using ConsoleMoneyTracker.src.main.DBContext;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.repository
{
    public class CategoryRepository : IRepository<CategoryDb, int>
    {
        private readonly SqliteDbContext _SqliteDbContext;
        public CategoryRepository()
        {
            _SqliteDbContext = new SqliteDbContext();
            _SqliteDbContext.Database.EnsureCreated();
        }
        public bool ContainsKey(int objID)
        {
            var exists = _SqliteDbContext.categoryDbs.Select(c=> c.ID).Contains(objID);
            return exists;
        }

        public void Delete(int objID)
        {
            var item = GetById(objID);
            if (Object.ReferenceEquals(item, null))
            {
                Console.WriteLine("This category id doen't exists in the current context");
            }
            else
            {
                _SqliteDbContext.categoryDbs.Remove(item);
                Save();
                Console.WriteLine("This category was deleted");
            }
        }

        public IEnumerable<CategoryDb> GetAll()
        {
            IEnumerable<CategoryDb> categoryDb = _SqliteDbContext.categoryDbs.AsEnumerable();
            return categoryDb;
        }

        public CategoryDb GetById(int objID)
        {
            var categoryDb = _SqliteDbContext.categoryDbs.Where(c => c.ID.Equals(objID)).FirstOrDefault();
            if (categoryDb == null) return new CategoryDb();

            return categoryDb;
        }

        public void Insert(CategoryDb obj)
        {
            _SqliteDbContext.categoryDbs.Add(obj);
            Save();
        }

        public void Save()
        {
            _SqliteDbContext.SaveChanges();
        }

        public void Update(CategoryDb obj)
        {
            var category = GetById(obj.ID);
            if (category == null)
            {
                Console.WriteLine("This category id doen't exists in the current context");
                return;
            }
            category.listItemId = obj.listItemId;
            _SqliteDbContext.categoryDbs.Update(category);
            Save();
        }
        public int Count()
        {
            return GetAll().Count();
        }
    }
}
