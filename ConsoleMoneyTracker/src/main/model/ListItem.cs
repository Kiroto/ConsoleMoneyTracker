using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class ListItem : IIndexable<int>
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string description { get; set; }
        public ConsoleColor foregroundColor { get; set; }
        public ConsoleColor backgroundColor { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime? removalDate { get; set; }
    }
}
