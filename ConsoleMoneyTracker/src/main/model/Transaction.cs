using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Transaction
    {
        public int id;
        public string description;
        public Category category;
        public Account sourceAccount;
        public Account targetAccount;
        public float amount;
        public float rate;
        public DateTime date;
    }
}
