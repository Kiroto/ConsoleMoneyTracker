using ConsoleMoneyTracker.src.main.model;
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
        private IRepository<Category, int> _categoryRepository;
        private IRepository<ListItem, int> _itemRepository;

        public CategoryController(IRepository<Category, int> categoryRepository, IRepository<ListItem, int> itemRepository)
        {
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }


        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAll().Where((it) => { return it.item.removalDate != null; }); // Only get non-deleted categories
        }

        public void InsertCategory(Category category)
        {
            _categoryRepository.Insert(category);
        }

        public void InsertCategory(string name, string shortName, string description, ConsoleColor fg, ConsoleColor bg)
        {
            Category cat = new Category();
            cat.item = new ListItem();
            cat.item.name = name;
            cat.item.description = description;
            cat.item.shortName = shortName;
            cat.item.creationDate = DateTime.Now;
            cat.item.foregroundColor = fg;
            cat.item.backgroundColor = bg;

            _categoryRepository.Insert(cat);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            category.item.removalDate = DateTime.Now;
            _categoryRepository.Update(category);
        }

        public int Count() { return _categoryRepository.Count(); }
    }
}
