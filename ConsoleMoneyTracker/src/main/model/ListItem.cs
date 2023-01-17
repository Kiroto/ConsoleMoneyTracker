using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class ListItem
    {
        public int id;
        public string name;
        public string shortName;
        public string description;
        public ConsoleColor foregroundColor;
        public ConsoleColor backgroundColor;
        public DateTime creationDate;
        public DateTime? removalDate;
    }
}
