using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.httpModel;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class CurrencyController
    {
        private IRepository<Currency, string> _currencyRepository;

        private ICurrencyInfoGetter _currencyInfoGetter;
        public CurrencyController(IRepository<Currency, string> repository, ICurrencyInfoGetter currencyInfoGetter)
        {
            _currencyRepository = repository;
            _currencyInfoGetter = currencyInfoGetter;
        }

        // Gets data 
        public Task<bool> updateCurrenciesFromInfoGetter()
        {
            Task<bool> t = new Task<bool>(delegate
            {
                // Get the currency rates
                CurrencyRatesView currencyRates = _currencyInfoGetter.getCurrencyRates();

                // Initialize this variable; might not need it.
                IList<CurrencyNameView>? currencyInfoView = null;

                // For every rate found...
                foreach (var currency in currencyRates.data)
                {

                    // If it exists in the repository
                    if (_currencyRepository.ContainsKey(currency.Key))
                    {
                        var existing = _currencyRepository.GetById(currency.Key);
                        // Just update the exchange rate
                        existing.toDollar = currency.Value;
                        _currencyRepository.Update(existing);
                    }
                    else
                    {   
                        // If not, build a new currency
                        // If the name and stuff hasn't been obtained yet...
                        if (currencyInfoView == null)
                        {

                            // Go get it 
                            currencyInfoView = _currencyInfoGetter.getCurrencyNames();

                            // If you couldn't deserialize it or for some reason an exception wasn't thrown and the code is still running...
                            if (currencyInfoView == null)
                            { 
                                // Literally 1984, we're not making any more requests on a loop lest we fully consume our API on accident.
                                break;
                            }
                        }
                        // Get the name from the info view
                        CurrencyNameView? ci = currencyInfoView.FirstOrDefault((item) =>
                        {
                            return item != null ? item.abbreviation == currency.Key : false;
                        }, defaultValue: null);

                        // If the name is not in the secondary api
                        if (ci == null)
                        {
                            // just skip to the next currency
                            continue;
                        }

                        // Finally create the currency and insert it to the repository
                        Currency newCurrency = new Currency(currency.Value, ci.currency, HttpUtility.HtmlDecode(ci.symbol), ci.abbreviation);
                        _currencyRepository.Insert(newCurrency);
                    };
                }
                return true;
            });
            t.Start();
            return t;
        }
    }
}
