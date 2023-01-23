using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMoneyTracker.src.main.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.repository;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.httpModel;
using System.Text.Json;
using Moq;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System.Security.Principal;

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class CurrencyControllerTests
    {
        // Mock CurrencyControllerRepository

        private InMemoryRepository<Currency, string> _currencyRepository = new InMemoryRepository<Currency, string>();
        private Mock<ICurrencyInfoGetter> currencyInfoGetterMock;
        private Mock<CurrencyController> currencyControllerMock;
        private CurrencyController controller;
        private ICurrencyInfoGetter currencyInfoGetter;
        private List<Currency> currencies;


        [TestInitialize]
        public void Setup() 
        {
            controller = new CurrencyController(_currencyRepository, currencyInfoGetter);

            currencyControllerMock = new Mock<CurrencyController>(MockBehavior.Strict);

            currencyControllerMock.CallBase = true;


            var dopListItem = new ListItem()
            {
                ID = 1,
                name = "Dominican Peso",
                shortName = "DOP",
                description = "This is dominican peso currency"
            };

            var dopCurrency = new Currency()
            {
                item = dopListItem,
                apiIdentifier = "DOP",
                lastUpdated = DateTime.Now,
                toDollar = float.Parse("53.75")
            };

            var COPListItem = new ListItem()
            {
                ID = 1,
                name = "Colombian Peso",
                shortName = "COP",
                description = "This is Colombian peso currency"
            };

            var copCurrency = new Currency()
            {
                item = COPListItem,
                apiIdentifier = "COP",
                lastUpdated = DateTime.Now,
                toDollar = float.Parse("65.75")
            };

            currencies = new List<Currency> { };
            currencies.Add(dopCurrency);
            currencies.Add(copCurrency);

        }

        [TestMethod()]
        public void CurrencyControllerTest()
        {
            var dopListItem = new ListItem()
            {
                ID = 1,
                name = "Dominican Peso",
                shortName = "DOP",
                description = "This is dominican peso currency"
            };

            var dopCurrency = new Currency()
            {
                item = dopListItem,
                apiIdentifier = "DOP",
                lastUpdated = DateTime.Now,
                toDollar = float.Parse("53.75")
            };

            var COPListItem = new ListItem()
            {
                ID = 1,
                name = "Colombian Peso",
                shortName = "COP",
                description = "This is Colombian peso currency"
            };

            var copCurrency = new Currency()
            {
                item = COPListItem,
                apiIdentifier = "COP",
                lastUpdated = DateTime.Now,
                toDollar = float.Parse("65.75")
            };

            currencies = new List<Currency> { };
            currencies.Add(dopCurrency);
            currencies.Add(copCurrency);

            Assert.IsTrue((currencies.Contains(dopCurrency)) && (currencies.Contains(copCurrency)));    

        }

        [TestMethod()]
        public void updateCurrenciesFromWebTest()
        {
            var vars = Environment.GetEnvironmentVariables();
            InMemoryRepository<Currency, string> currencyRepository = new InMemoryRepository<Currency, string>();
            CurrencyController sut = new CurrencyController(currencyRepository, new OnlineCurrencyInfoGetter());
            Task<bool> resultCompleted = sut.updateCurrenciesFromInfoGetter();

            bool isOK = resultCompleted.Result;

            Assert.IsTrue(isOK);
            Assert.IsTrue(currencyRepository.GetAll().Count() != 0);
        }
    }
}