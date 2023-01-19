using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Currency : IIndexable<int>
    {
        public int ID { get; set; }
        public ListItem item;
        public string apiIdentifier;
        public float toDollar;
    }
}
