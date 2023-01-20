using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Configuration : IIndexable<string>
    {
        public string ID { get => key; set { key = value; } }
        public string key;
        public string value;
    }
}
