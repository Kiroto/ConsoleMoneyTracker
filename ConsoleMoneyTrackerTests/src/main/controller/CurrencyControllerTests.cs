﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class CurrencyControllerTests
    {
        [TestMethod()]
        public void CurrencyControllerTest()
        {
            Assert.Fail();
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