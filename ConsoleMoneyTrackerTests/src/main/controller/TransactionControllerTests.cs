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
    public class TransactionControllerTests
    {
        // Mock TransactionControllerRepository
        private InMemoryRepository<Transaction, int> _transactionRepository;
        private InMemoryRepository<ListItem, int> _listItemRepository;
        private InMemoryRepository<Account, int> _accountRepository;

        private Mock<TransactionController> transactionControllerMock;

        private TransactionController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new TransactionController(_transactionRepository, _listItemRepository, _accountRepository);

            transactionControllerMock = new Mock<TransactionController>(MockBehavior.Strict);

            transactionControllerMock.CallBase = true;

            ListItem listItem = new ListItem()
            {
                ID = 1,
                name = "Jerry paga renta",
                shortName = "JerryRenta",
                description = "Pago quincenal de renta de Jerry Rivas"
            };

            ListItem rentCategory = new ListItem()
            {
                ID = 1,
                name = "Renta",
                shortName = "Renta",
                description = "Pago de renta"
            };

            Category category = new Category()
            {
                ID = 1,
                item = rentCategory,
            };

            Transaction transaction = new Transaction()
            {
                item = listItem,
                category = category,
                amount = float.Parse("326666.122"),
                rate = float.Parse("52.54"),
            };

            ListItem dopListItem = new ListItem()
            {
                ID = 1,
                name = "Dominican Peso",
                shortName = "DOP",
                description = "This is dominican peso currency"
            };

            Currency dopCurrency = new Currency()
            {
                item = dopListItem,
                apiIdentifier = "DOP",
                lastUpdated = DateTime.Now,
                toDollar = float.Parse("53.75")
            };

            var accountListItem = new ListItem()
            {
                ID = 1,
                name = "Cuenta de ahorros",
                shortName = "Mi cuenta de ahorros",
                description = "Esta es mi cuenta de ahorros"
            };

            Account account = new Account()
            {
                ID = 1,
                amount = float.Parse("900999.23"),
                item = accountListItem,
                currency = dopCurrency,
            };

            ListItem listItem2 = new ListItem()
            {
                ID = 2,
                name = "Cuenta de retiro",
                shortName = "Mi cuenta de retiro",
                description = "Esta es mi cuenta de retiro"
            };


            transactionControllerMock.Setup(t => t.MakeTransaction(account, null, 10000.32f, category, "")).Returns(transaction);

                transactionControllerMock.Setup(t => t.CommitTransaction(transaction)).Verifiable();
                transactionControllerMock.Setup(t => t.UpdateTransaction(transaction)).Verifiable();
                transactionControllerMock.Setup(t => t.DeleteTransaction(transaction)).Verifiable();
            }
        }
 }