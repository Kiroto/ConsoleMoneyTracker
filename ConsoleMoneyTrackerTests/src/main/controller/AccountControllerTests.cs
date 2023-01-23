using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMoneyTracker.src.main.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        // Mock AccountControllerRepository
        static InMemoryRepository<Account, int> _accountRepository = new InMemoryRepository<Account, int>();

        _accountRepository = new InMemoryRepository<Account, int>();

        [TestMethod]
        public void CreateAccount()
        {
            try
            {
                Account account1 = new Account(1, listItem1, dominicanPeso, 320);
            }
            catch (Exception e)
            {
                Assert.Fail("Account creation failed.");
            }
        }

        [TestMethod]
        public void CreateAccounts()
        {
 
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

        [TestMethod]
        public void ConvertTransactionsCurrency()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAccountsActualBalance()
        {
            Assert.Fail();
        }
    }
}