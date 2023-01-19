using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class CurrencyController
    {
        private IRepository<Currency, int> _currencyRepository;
        public CurrencyController(IRepository<Currency, int> repository) {
            _currencyRepository= repository;
        }

        public Task<bool> updateCurrenciesFromWeb()
        {
            return Task.FromResult(true); // TODO
        }
    }
}
