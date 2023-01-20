using ConsoleMoneyTracker.src.main.model.httpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Currency : IIndexable<string>
    {
        public string ID { get => apiIdentifier; private set { apiIdentifier = value; } }
        public ListItem item;
        public string apiIdentifier;
        public DateTime lastUpdated;
        public float toDollar;

        public Currency(float rate, string name, string shortName, string code)
        {
            toDollar = rate;
            ID = code;

            item = new ListItem();

            item.name = name;
            item.shortName = shortName;
            item.description = "";

            // Hardcoded defaults
            item.creationDate = DateTime.Now;
            lastUpdated = DateTime.Now;

            item.foregroundColor = ConsoleColor.White;
            item.backgroundColor = ConsoleColor.Black;
        }
    }
}
