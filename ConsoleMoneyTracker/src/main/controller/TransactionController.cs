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
    public class TransactionController
    {
        private IRepository<TransactionDb, int> _transactionRepository; // All transactions from a newly removed account should be removed

        public TransactionController(IRepository<TransactionDb, int> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        public IEnumerable<TransactionDb> GetTransactions()
        {
            return _transactionRepository.GetAll().Where((it) => { return it.item.removalDate == null; }); // Only get non-deleted accounts
        }

        public void MakeTransaction(AccountDb? sourceAccount, AccountDb? targetAccount, float amount, CategoryDb category)
        {
            if (sourceAccount == null && targetAccount == null)
            {
                throw new ArgumentNullException($"{nameof(sourceAccount)} and {nameof(targetAccount)}");
            }
            TransactionDb newTransaction = new TransactionDb();
            newTransaction.amount = amount;
            newTransaction.sourceAccountId=  sourceAccount.ID;
            newTransaction.targetAccountId = targetAccount.ID;
            newTransaction.category= category;

            newTransaction.item = new ListItemDb();
            newTransaction.item.creationDate = DateTime.Now;
            newTransaction.rate = 1;

            if (sourceAccount == null)
            { // Income
                newTransaction.item.foregroundColor = ConsoleColor.Green;
            }
            else if (targetAccount == null)
            { // Expense
                newTransaction.item.foregroundColor = ConsoleColor.Red;
            }
            else
            { // Transference
                newTransaction.item.foregroundColor = ConsoleColor.Yellow;
                newTransaction.rate = sourceAccount.currency.toDollar / targetAccount.currency.toDollar;
            }

            _transactionRepository.Insert(newTransaction);
        }

        public void UpdateTransaction(TransactionDb transaction)
        {
            _transactionRepository.Update(transaction);
        }

        public void DeleteTransaction(TransactionDb transaction)
        {
            transaction.item.removalDate = DateTime.Now;
            _transactionRepository.Update(transaction);
        }
    }
}
