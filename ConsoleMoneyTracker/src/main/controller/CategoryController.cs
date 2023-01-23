using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.dbModel;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class CategoryController
    {
        private IRepository<CategoryDb, int> _categoryRepository;

        public CategoryController(IRepository<CategoryDb, int> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public IEnumerable<CategoryDb> GetCategories()
        {
            return _categoryRepository.GetAll().Where((it) => { return it.item.removalDate == null; }); // Only get non-deleted categories
        }

        public void InsertCategory(CategoryDb category)
        {
            _categoryRepository.Insert(category);
        }

        public void InsertCategory(string name, string shortName, string description, ConsoleColor fg, ConsoleColor bg)
        {
            CategoryDb cat = new CategoryDb();
            cat.item = new ListItemDb();
            cat.item.name = name;
            cat.item.description = description;
            cat.item.shortName = shortName;
            cat.item.creationDate = DateTime.Now;
            cat.item.foregroundColor = fg;
            cat.item.backgroundColor = bg;

            _categoryRepository.Insert(cat);
        }

        public void UpdateCategory(CategoryDb category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(CategoryDb category)
        {
            category.item.removalDate = DateTime.Now;
            _categoryRepository.Update(category);
        }
    }
}
