using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Category : IIndexable<int>
    {
        public int ID { get; set; }
        public ListItem item { get; set; }
    }
}
