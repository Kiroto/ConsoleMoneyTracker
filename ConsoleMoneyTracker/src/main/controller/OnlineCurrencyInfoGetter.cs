using ConsoleMoneyTracker.src.main.model.httpModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class OnlineCurrencyInfoGetter : ICurrencyInfoGetter
    {
        private static string currencyRatesAPI = "https://api.exchangerate.host/latest?base=USD";
        private static string currencyNamesAPI = "https://gist.githubusercontent.com/stevekinney/8334552/raw/28d6e58f99ba242b7f798a27877e2afce75a5dca/currency-symbols.json";
        public IList<CurrencyNameView> getCurrencyNames()
        {
            // Go get it
            Task<string> currencyNames = HttpWrapper.HttpGet(currencyNamesAPI);

            string jsonStringResult = currencyNames.Result;

            // Deserialize it
            IList<CurrencyNameView>? currencyNamesView = JsonConvert.DeserializeObject<IList<CurrencyNameView>>(jsonStringResult);

            if (currencyNamesView == null)
            {
                throw new Exception("Could not deserialize json");
            }

            return currencyNamesView;
        }

        public CurrencyRatesView getCurrencyRates()
        {
            Task<string> currencyData = HttpWrapper.HttpGet(currencyRatesAPI);

            string jsonStringResult = currencyData.Result;
            CurrencyRatesView? currencyRates = JsonConvert.DeserializeObject<CurrencyRatesView>(jsonStringResult);
            if (currencyRates == null)
            {
                throw new Exception("Could not deserialize json");
            }
            return currencyRates;
        }
    }
}
