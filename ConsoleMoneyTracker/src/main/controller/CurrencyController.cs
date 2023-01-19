using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.httpModel;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public Task<bool> updateCurrenciesFromWeb()
        {
            Task<bool> t = new Task<bool>(delegate
            {
                // Get the currency rates
                CurrencyRatesView currencyRates = _currencyInfoGetter.getCurrencyRates();

                // Initialize this variable; might not need it.
                CurrencyInfoView? currencyInfoView = null;

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
                            currencyInfoView = _currencyInfoGetter.getCurrencyInfo();

                            // If you couldn't deserialize it or for some reason an exception wasn't thrown and the code is still running...
                            if (currencyInfoView == null)
                            { 
                                // Literally 1984, we're not making any more requests on a loop lest we fully consume our API on accident.
                                break;
                            }
                        }
                        // Should have the currency info view by now
                        CurrencyInformation ci = currencyInfoView.data[currency.Key];

                        // If for some reason there is value information but no name information 
                        if (ci == null)
                        {
                            // just skip to the next currency
                            continue;
                        }

                        // Finally create the currency and insert it to the repository
                        Currency newCurrency = new Currency(ci, currency.Value);
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
