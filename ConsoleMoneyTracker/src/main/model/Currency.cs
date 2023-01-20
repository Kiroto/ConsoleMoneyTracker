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
    }
}
