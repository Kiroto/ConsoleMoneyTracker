using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Transaction : IIndexable<int>
    {
        public int ID { get; set; }
        public ListItem item;
        public Category category;
        public Account sourceAccount;
        public Account targetAccount;
        public float amount;
        public float rate;
    }
}
