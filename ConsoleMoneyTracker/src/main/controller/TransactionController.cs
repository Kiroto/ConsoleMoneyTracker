using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class TransactionController
    {
        private IRepository<Transaction, int> _transactionRepository; // All transactions from a newly removed account should be removed
        private IRepository<Account, int> _accountRepository;
        private IRepository<ListItem, int> _listItemRepository;

        public TransactionController(IRepository<Transaction, int> transactionRepository, IRepository<ListItem, int> itemRepository, IRepository<Account, int> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _listItemRepository = itemRepository;
            _accountRepository = accountRepository;
        }


        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactionRepository.GetAll().Where((it) => { return it.item.removalDate == null; }); // Only get non-deleted accounts
        }

        public Transaction MakeTransaction(Account? sourceAccount, Account? targetAccount, float amount, Category category, string description)
        {
            if (sourceAccount == null && targetAccount == null)
            {
                throw new ArgumentNullException($"{nameof(sourceAccount)} and {nameof(targetAccount)}");
            }
            Transaction newTransaction = new Transaction();
            newTransaction.amount = amount;
            newTransaction.sourceAccount = sourceAccount;
            newTransaction.targetAccount = targetAccount;
            newTransaction.category= category;

            newTransaction.item = new ListItem();
            newTransaction.item.description= description;
            newTransaction.item.creationDate = DateTime.Now;
            newTransaction.rate = 1;

            if (sourceAccount == null)
            { // Income
                newTransaction.item.shortName = "[INC]";
                newTransaction.item.foregroundColor = ConsoleColor.Green;
            }
            else if (targetAccount == null)
            { // Expense
                newTransaction.item.shortName = "[EXP]";
                newTransaction.item.foregroundColor = ConsoleColor.Red;
            }
            else
            { // Transference
                newTransaction.item.shortName = "[TRN]";
                newTransaction.item.foregroundColor = ConsoleColor.Yellow;
                newTransaction.rate = sourceAccount.currency.toDollar / targetAccount.currency.toDollar;
            }

            return newTransaction;
        }

        public void CommitTransaction(Transaction transaction)
        {
            _listItemRepository.Insert(transaction.item);
            _transactionRepository.Insert(transaction);
            if (transaction.targetAccount != null)
            {
                float transferAmt = transaction.amount * transaction.rate;
                transaction.targetAccount.amount -= transferAmt;
                _accountRepository.Update(transaction.targetAccount);
            }
            if (transaction.sourceAccount != null)
            {
                transaction.sourceAccount.amount -= transaction.amount;
                _accountRepository.Update(transaction.sourceAccount);
            }
            _transactionRepository.Insert(transaction);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _transactionRepository.Update(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            transaction.item.removalDate = DateTime.Now;
            _transactionRepository.Update(transaction);
        }
    }
}
