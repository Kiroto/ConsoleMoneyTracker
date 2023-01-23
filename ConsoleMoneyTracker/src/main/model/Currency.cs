using ConsoleMoneyTracker.src.main.model.httpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Currency : IIndexable<string>, IListable
    {
        ListItem IListable.item { get => item; set => item = value; }
        public string ID { get => apiIdentifier; set { apiIdentifier = value; } }
        public ListItem item { get; set; }
        public string apiIdentifier { get; set; }
        public DateTime lastUpdated { get; set; }
        public  float toDollar { get; set; }
    }
}
