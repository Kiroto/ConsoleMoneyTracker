using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public class Account : IIndexable<int>, IListable
    {
        ListItem IListable.item { get => item; set => item = value; }
        public int ID { get; set; }

        public ListItem item;
        public Currency currency;
        public float amount;

    }
}
