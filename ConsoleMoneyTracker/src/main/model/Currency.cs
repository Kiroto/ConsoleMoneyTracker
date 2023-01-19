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
        public string ID { get => apiIdentifier; set { apiIdentifier = value; } }
        public ListItem item;
        public string apiIdentifier;
        public DateTime lastUpdated;
        public float toDollar;

        public Currency(CurrencyNameView ci, float rate) {
            toDollar = rate;
            ID = ci.abbreviation;
            lastUpdated = DateTime.Now;

            item = new ListItem();

            item.name = ci.currency;
            item.shortName = HttpUtility.HtmlDecode(ci.symbol);
            item.description = "";

            item.creationDate = DateTime.Now;

            // Hardcoded defaults
            item.foregroundColor = ConsoleColor.White;
            item.backgroundColor = ConsoleColor.Black;
        }
    }
}
