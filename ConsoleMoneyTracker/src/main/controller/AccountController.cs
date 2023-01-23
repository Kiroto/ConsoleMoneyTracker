using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.dbModel;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class AccountController
    {
        private IRepository<AccountDb, int> _accountRepository;
        private IRepository<TransactionDb, int> _transactionRepository; // All transactions from a newly removed account should be removed

        public AccountController(IRepository<AccountDb, int> accountRepository, IRepository<TransactionDb, int> transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }


        public IEnumerable<AccountDb> GetAccounts()
        {
            return _accountRepository.GetAll().Where((it) => { return it.item.removalDate == null; }); // Only get non-deleted accounts
        }

        public void InsertAccount(AccountDb account)
        {
            _accountRepository.Insert(account);
        }

        public void InsertAccount(string name, string shortName, string description, CurrencyDb currency, ConsoleColor fg, ConsoleColor bg)
        {
            AccountDb acc = new AccountDb();
            acc.item = new ListItemDb();
            acc.item.name = name;
            acc.item.description = description;
            acc.item.shortName = shortName;
            acc.item.creationDate = DateTime.Now;
            acc.item.foregroundColor = fg;
            acc.item.backgroundColor = bg;
            acc.amount = 0;
            acc.currency = currency;

            _accountRepository.Insert(acc);
        }

        public void UpdateAccount(AccountDb account)
        {
            _accountRepository.Update(account);
        }

        public void DeleteAccount(AccountDb account)
        {
            account.item.removalDate = DateTime.Now;
            _accountRepository.Update(account);
            // "remove" all the transactions this account has.
            IEnumerable<TransactionDb> relevantTransactions = _transactionRepository.GetAll().Where((it) => it.sourceAccountId == account.ID || it.targetAccountId == account.ID);
            foreach (TransactionDb transaction in relevantTransactions) {
                transaction.item.removalDate = DateTime.Now;
            }
        }
    }
}
