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
        public string description { get; set; }
        public Category category { get; set; }
        public Account sourceAccount { get; set; }
        public Account targetAccount { get; set; }
        public float amount { get; set; }
        public float rate { get; set; }
        public DateTime date { get; set; }
    }
}
