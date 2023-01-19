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
        private static string currencyAPIURI = "https://api.freecurrencyapi.com/v1";
        private static string currencyAPILatestEndpoint = "latest";
        private static string currencyAPICurrenciesEndpoint = "currencies";
        private static string currencyAPIApiKeyKey = "apikey";
        private static string currencyAPIApiKeyValue =  Environment.GetEnvironmentVariable("freeCurrencyApiKey");
        public CurrencyInfoView getCurrencyInfo()
        {
            // Go get it
            Task<string> currencyNames = HttpWrapper.HttpGet($"{currencyAPIURI}/{currencyAPICurrenciesEndpoint}?{currencyAPIApiKeyKey}={currencyAPIApiKeyValue}");

            string jsonStringResult = currencyNames.Result;

            // Deserialize it
            CurrencyInfoView? currencyInfoView = JsonConvert.DeserializeObject<CurrencyInfoView>(jsonStringResult);

            if (currencyInfoView == null)
            {
                throw new Exception("Could not deserialize json");
            }

            return currencyInfoView;
        }

        public CurrencyRatesView getCurrencyRates()
        {
            Task<string> currencyData = HttpWrapper.HttpGet($"{currencyAPIURI}/{currencyAPILatestEndpoint}?{currencyAPIApiKeyKey}={currencyAPIApiKeyValue}");

            string jsonStringResult = currencyData.Result;
            CurrencyRatesView? currencyRates = JsonConvert.DeserializeObject<CurrencyRatesView>(jsonStringResult);
            if (currencyRates == null )
            {
                throw new Exception("Could not deserialize json");
            }
            return currencyRates;
        }
    }
}
