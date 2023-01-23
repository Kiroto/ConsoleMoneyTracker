using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class AccountController
    {
        private IRepository<Account, int> _accountRepository;
        private IRepository<ListItem, int> _itemRepository;
        private IRepository<Transaction, int> _transactionRepository; // All transactions from a newly removed account should be removed
        public AccountController(IRepository<Account, int> accountRepository, IRepository<Transaction, int> transactionRepository, IRepository<ListItem, int> itemRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAll().Where((it) => { return it.item.removalDate == null; }); // Only get non-deleted accounts
        }

        public void InsertAccount(Account account)
        {
            _accountRepository.Insert(account);
        }

        public void InsertAccount(string name, string shortName, string description, Currency currency, float startingMoney = 0, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Account acc = new Account();
            acc.item = new ListItem();
            acc.item.name = name;
            acc.item.description = description;
            acc.item.shortName = shortName;
            acc.item.creationDate = DateTime.Now;
            acc.item.foregroundColor = fg;
            acc.item.backgroundColor = bg;
            acc.amount = startingMoney;
            acc.currency = currency;

            _itemRepository.Insert(acc.item);
            _accountRepository.Insert(acc);
        }

        public void UpdateAccount(Account account)
        {
            _itemRepository.Update(account.item);
            _accountRepository.Update(account);
        }

        public void DeleteAccount(Account account)
        {
            account.item.removalDate = DateTime.Now;
            _itemRepository.Update(account.item);
            _accountRepository.Update(account);
            // "remove" all the transactions this account has.
            IEnumerable<Transaction> relevantTransactions = _transactionRepository.GetAll().Where((it) => it.sourceAccount.ID == account.ID || it.targetAccount.ID == account.ID);
            foreach (Transaction transaction in relevantTransactions) {
                transaction.item.removalDate = DateTime.Now;
            }
        }

        public int Count()
        {
            return GetAccounts().Count();
        }
    }
}
