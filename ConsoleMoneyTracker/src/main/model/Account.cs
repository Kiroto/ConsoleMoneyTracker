using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    internal class Account
    {
        public int id;
        public ListItem item;
        public Currency currency;
        public float amount;
    }
}
