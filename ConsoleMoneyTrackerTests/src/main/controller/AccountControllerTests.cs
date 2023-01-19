using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMoneyTracker.src.main.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.model;

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void GetAccounts()
        {
            // From some accounts
            var dopListItem = new ListItem(1, "Dominican Peso", "DOP", "Dominican peso currency");

            var dominicanPeso = new Currency(1, dopListItem, "", 51.2);

            var listItem1 = new ListItem(1, "Cuenta 1", "C-01", "Cuenta de ahorros 1");
            var listItem2 = new ListItem(2, "Cuenta 2", "C-02", "Cuenta de ahorros 2");

            var account1 = new Account(1, listItem1, dominicanPeso, 320);
            var account2 = new Account(2, listItem2, dominicanPeso, 1250.63);

            // Return an array of Account
            var accountController = new AccountController(account1, account2);

            Assert.AreEqual(accountController.GetAccounts(), new Account[] {account1, account2});
        }

        [TestMethod]
        public void CreateAccounts()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateAccounts()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteAccounts()
        {
            Assert.Fail();
        }
    }
}