using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Account : IIndexable<int>
    {
        public int ID { get; set; }
        public ListItem item { get; set; }
        public Currency currency { get; set; }
        public float amount { get; set; }   

    }
}
