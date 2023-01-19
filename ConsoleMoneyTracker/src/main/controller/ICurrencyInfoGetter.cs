using ConsoleMoneyTracker.src.main.model.httpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public interface ICurrencyInfoGetter
    {
        public CurrencyRatesView getCurrencyRates();
        public IList<CurrencyNameView> getCurrencyNames();
    }
}
