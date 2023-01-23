using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMoneyTracker.src.main.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using Moq;
using ConsoleMoneyTracker.src.main.model.dbModel;
using System.Security.Principal;

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        // Mock AccountControllerRepository
        private InMemoryRepository<Account, int> _accountRepository = new InMemoryRepository<Account, int>();
        private InMemoryRepository<Transaction, int> _transactionRepository = new InMemoryRepository<Transaction, int>();
        private InMemoryRepository<ListItem, int> _itemRepository = new InMemoryRepository<ListItem, int>();
        private Mock<AccountController>? accountControllerMock;
        private AccountController? controller;
        private List<Account>? accounts;

        [TestInitialize]
        public void Setup()
        {
            controller = new AccountController(_accountRepository, _transactionRepository, _itemRepository);

            accountControllerMock = new Mock<AccountController>;

            accounts = new List<Account>();

            Account account = new Account(1, null, null, 3000.21);

            accounts.Add(account);

            accountControllerMock.Setup(a => a.GetAccounts()).Returns(accounts);
            accountControllerMock.Setup(a => a.InsertAccount(a)).Returns(account);
            accountControllerMock.Setup(a => a.UpdateAccount(a)).Returns(updatedAccount);
            accountControllerMock.Setup(a => a.DeleteAccount(a)).Returns(deletedAccount);
        }

        [TestMethod]
        public void CreateAccountTest()
        {
            Assert.IsNotNull(controller.GetAccounts());
        }

        [TestMethod]
        public void InsertAccountTest()
        {
            var accountToBeInserted = new Account(1, null, null, 3000.21);

            controller.InsertAccount(accountToBeInserted);
        }

        [TestMethod]
        public void UpdateAccounts()
        {
            var accountToBeUpdated = new Account(1, null, null, 400.09);

            controller.InsertAccount(accountToBeUpdated);
        }

        [TestMethod]
        public void DeleteAccounts()
        {
            var accountToBeDeleted = new Account(1, null, null, 400.09);

            controller.DeleteAccount(accountToBeDeleted);
        }

        [TestMethod]
        public void GetAccountsActualBalance()
        {
            var accounts = controller.GetAccounts();

            foreach(var account in accounts)
            {
                Assert.Equals(account.amount, 3000.21);
            }
        }
    }
}