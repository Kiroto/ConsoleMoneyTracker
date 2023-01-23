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

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class TransactionControllerTests
    {
        // Mock AccountControllerRepository
        private InMemoryRepository<Account, int> _accountRepository = new InMemoryRepository<Account, int>();
        private InMemoryRepository<Transaction, int> _transactionRepository = new InMemoryRepository<Transaction, int>();
        private InMemoryRepository<ListItem, int> _itemRepository = new InMemoryRepository<ListItem, int>();

        [TestMethod()]
        public void GettingTransactions()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MakingATransactionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ComittingATransactionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdatingOneTransactionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeletingOneTransactionTest()
        {
            Assert.Fail();
        }
    }
}