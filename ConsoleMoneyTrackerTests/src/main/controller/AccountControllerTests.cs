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

        private Mock<AccountController> accountControllerMock;

        private AccountController controller;
        private List<Account> accounts;

        [TestInitialize]
        public void Setup()
        {
            controller = new AccountController(_accountRepository, _transactionRepository, _itemRepository);

            accountControllerMock = new Mock<AccountController>(MockBehavior.Strict);

            accountControllerMock.CallBase = true;

            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account account = new Account()
            {
                ID = 1,
                amount = float.Parse("900999.23"),
                item = listItem1,
                currency = dopCurrency,
            };

            var listItem2 = new ListItem()
            {
                ID = 2,
                name = "Cuenta de retiro",
                shortName = "Mi cuenta de retiro",
                description = "Esta es mi cuenta de retiro"
            };

            Account account2 = new()
            {
                ID = 2,
                amount = float.Parse("3999.23"),
                item = listItem2,
                currency = dopCurrency,
            };

            accounts = new List<Account> { };

            accounts.Add(account);
            accounts.Add(account2);

            accountControllerMock.Setup(a => a.GetAccounts()).Returns(accounts.AsEnumerable());

            //accountControllerMock.Setup(a => a.GetAccounts()).Returns((IEnumerable<Account>) accounts);

            accountControllerMock.Setup(a => a.InsertAccount(account)).Verifiable();
            accountControllerMock.Setup(a => a.UpdateAccount(account)).Verifiable();
            accountControllerMock.Setup(a => a.DeleteAccount(account)).Verifiable();
            
            accountControllerMock.Setup(a => a.Count()).Returns(2);
        }

        [TestMethod]
        public void CreateAccountTest()
        {
            Assert.IsNotNull(controller.GetAccounts());
        }

        [TestMethod]
        public void InsertAccountTest()
        {
            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account account = new Account()
            {
                ID = 1,
                amount = float.Parse("900999.23"),
                item = listItem1,
                currency = dopCurrency,
            };

            controller.InsertAccount(account);
        }

        [TestMethod]
        public void UpdateAccounts()
        {
            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account accountToBeUpdated = new Account()
            {
                ID = 1,
                amount = float.Parse("0.2"),
                item = listItem1,
                currency = dopCurrency,
            };

            controller.UpdateAccount(accountToBeUpdated);
        }

        [TestMethod]
        public void DeleteAccounts()
        {
            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account accountToBeDeleted = new Account()
            {
                ID = 1,
                amount = float.Parse("0.2"),
                item = listItem1,
                currency = dopCurrency,
            };

            controller.DeleteAccount(accountToBeDeleted);
        }

        [TestMethod]
        public void GetAccountsActualBalance()
        {
            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account accountToBeDeleted = new Account()
            {
                ID = 1,
                amount = float.Parse("0.2"),
                item = listItem1,
                currency = dopCurrency,
            };

            var accounts = controller.GetAccounts();

            foreach(var account in accounts)
            {
                Assert.Equals(account.amount, 0.2);
            }
        }

        [TestMethod]
        public void CountAccountsTest()
        {
            var listItem1 = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

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

            Account account = new Account()
            {
                ID = 1,
                amount = float.Parse("900999.23"),
                item = listItem1,
                currency = dopCurrency,
            };

            var listItem2 = new ListItem()
            {
                ID = 2,
                name = "Cuenta de retiro",
                shortName = "Mi cuenta de retiro",
                description = "Esta es mi cuenta de retiro"
            };

            Account account2 = new()
            {
                ID = 2,
                amount = float.Parse("3999.23"),
                item = listItem2,
                currency = dopCurrency,
            };

            controller.InsertAccount(account);
            controller.InsertAccount(account2);

            Assert.AreEqual(controller.Count(), 2);
        }
    }
}