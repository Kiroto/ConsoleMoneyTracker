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
        ListItem dopListItem = new (1, "Dominican Peso", "DOP", "Dominican peso currency");

        Currency dominicanPeso = new (1, dopListItem, "", 51.2);

        ListItem listItem1 = new (1, "Cuenta 1", "C-01", "Cuenta de ahorros 1");
        ListItem listItem2 = new (2, "Cuenta 2", "C-02", "Cuenta de ahorros 2");

        Account account1 = new(1, listItem1, dominicanPeso, 320);
        Account account2 = new(2, listItem2, dominicanPeso, 1250.63);

        Account account2 = new (2, listItem2, dominicanPeso, 1250.63);

        [TestMethod]
        public void CreateAccount()
        {
            try
            {
                Account account1 = new (1, listItem1, dominicanPeso, 320);
            }
            catch (Exception e)
            {
                Assert.Fail("Account creation failed.");
            }
        }

        [TestMethod]
        public void CreateAccounts()
        {
            try
            {
                Account account1 = new (1, listItem1, dominicanPeso, 320);
                Account account2 = new (2, listItem2, dominicanPeso, 1250.63);
            }
            catch (Exception e)
            {
                Assert.Fail("Account creation failed.");
            }
        }


        [TestMethod()]
        public void GetAccounts()
        {
            // Return an array of Account
            var accountController = new AccountController(account1, account2);

            Assert.AreEqual(accountController.GetAccounts(), new Account[] {account1, account2});
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