using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Category : IIndexable<int>
    {
        public int ID { get; private set; }
        public ListItem item;
    }
}
