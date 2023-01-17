using ConsoleMoneyTracker.src.main.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = ConsoleMoneyTracker.src.main.model.Transaction;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class TransactionController
    {
        public IList<Transaction>? Transactions;

        public IList<Transaction> GetTransactions()
        {
            throw null;
        }
    }
}
