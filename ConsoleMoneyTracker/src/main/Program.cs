using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.model.dbModel;
using ConsoleMoneyTracker.src.main.repository;
using Spectre.Console;
using System.Xml.Linq;

namespace ConsoleMoneyTracker.src.main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "Pedro";

            IRepository<Account, int> accountsRepository =  new AccountRepository();
            IRepository<Category, int> categoryRepository = (IRepository<Category, int>) new CategoryRepository();
            IRepository<Currency, string> currencyRepository = (IRepository<Currency, string>) new CurrencyRepository();
            IRepository<Transaction, int> transactionRepository =(IRepository<Transaction, int>) new TransactionRepository();
            IRepository<ListItem, int> listItemRepository =(IRepository<ListItem, int>) new ListItemRepository();
            while (true)
            {
                List<string> list = new List<string>()
                {
                    "Add Transaction", "Manage Information", "See a Report", "Exit",
                };

                var selectionText = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Welcome [blue]{userName}[/]! What would you like to do?")
                        .PageSize(10)
                        .AddChoices(list));
                int selection = list.IndexOf(selectionText);

                if (selection == 0)
                {

                    MakeTransactionPipeline(accountsRepository, categoryRepository, transactionRepository);
                }
            }
        }

        static void MakeTransactionPipeline(IRepository<Account, int> acr, IRepository<Category, int> catr, IRepository<Transaction, int> transr)
        {
            int accountCount = acr.Count();
            // Validate if a transaction is possible
            if (accountCount <= 0)
            {
                errorBox("You need at least [red]one[/] [green]account[/] to make transactions.");
                return;
            }
            if (catr.Count() <= 0)
            {
                errorBox("You need at least [red]one[/] [olive]category[/] to make transactions.");
                return;
            }

            int transactionType = selectOption(new List<string>() { "Expense", "Income", "Movement" }, "What kind of transaction?");

            Transaction transaction = new Transaction();
            transaction.item = new ListItem();

            List<Account> selectableAccounts = acr.GetAll().ToList();

            transaction.rate = 1;

            switch (transactionType)
            {
                case 0:
                    transaction.item.shortName = "[EXP]";
                    transaction.sourceAccount = selectAccount(selectableAccounts, "Select the source of the expense");
                    break;
                case 1:
                    transaction.item.shortName = "[INC]";
                    transaction.targetAccount = selectAccount(selectableAccounts, "Select the target of the income.");
                    break;
                case 2:
                    if (accountCount < 2)
                    {
                        errorBox("You need at least [red]two[/] [olive]accounts[/] to make movements.");
                        return;
                    }
                    transaction.item.shortName = "[MOV]";
                    transaction.sourceAccount = selectAccount(selectableAccounts, "Select the source of the movement.");
                    selectableAccounts.Remove(transaction.sourceAccount);
                    transaction.targetAccount = selectAccount(selectableAccounts, "Select the target of the movement.");
                    transaction.rate = transaction.sourceAccount.currency.toDollar / transaction.targetAccount.currency.toDollar;
                    // get both
                    break;
                default:
                    // error
                    break;
            }

            transaction.amount = AnsiConsole.Ask<float>($"How much shall be transferred?{(transaction.sourceAccount != null ? "(Available: [blue]" + transaction.sourceAccount.amount + "[/]" : " ")}");
            if (transaction.sourceAccount != null && transaction.amount > transaction.sourceAccount.amount)
            {
                errorBox("There isn't enough balance on this account for this transference");
                return;
            }

            List<Category> selectableCategories = catr.GetAll().ToList();

            transaction.category = selectCategory(selectableCategories, "Select the category of this transaction.");
            transaction.item.description = AnsiConsole.Ask<string>("Write a [green]description[/] for this transaction.");
            transaction.item.creationDate = DateTime.Now;


            var table = new Table();
            // Add some columns
            table.AddColumn("Item");
            table.AddColumn("Value");

            // Add some rows
            table.AddRow("Transaction Category", transaction.category.item.name);
            if (transaction.sourceAccount != null)
            {
                table.AddRow("Source Account", transaction.sourceAccount.item.name);
                table.AddRow("Outgoing Amount", transaction.amount.ToString());
                table.AddRow("New Source Balance", (transaction.sourceAccount.amount - transaction.amount).ToString());
            }
            if (transaction.rate != 1)
            {
                table.AddRow("Transaction Rate", transaction.rate.ToString());
            }
            if (transaction.targetAccount != null)
            {
                table.AddRow("Target Account", transaction.targetAccount.item.name);
                float incomingAmt = transaction.amount * transaction.rate;
                table.AddRow("Incoming Amount", incomingAmt.ToString());
                table.AddRow("New Target Balance", (transaction.targetAccount.amount + incomingAmt).ToString());
            }

            // Render the table to the console
            AnsiConsole.Write(table);

            if (AnsiConsole.Confirm("Are these OK?"))
            {
                EffectuateTransaction(transaction, acr,  transr);
            };
        }

        // TODO: can merge item selection thingy
        static Account selectAccount(IList<Account> accountList, string prompt)
        {
            var accountSelectionList = accountList.Select((it) => { return $"{it.ID}. {it.item.name}"; }).ToList();
            int selectedIndex = selectOption(accountSelectionList, prompt);

            return accountList[selectedIndex];
        }

        static Category selectCategory(IList<Category> categoryList, string prompt)
        {
            var accountSelectionList = categoryList.Select((it) => { return $"{it.ID}. {it.item.name}"; }).ToList();
            int selectedIndex = selectOption(accountSelectionList, prompt);

            return categoryList[selectedIndex];
        }

        static int selectOption(IList<string> options, string prompt)
        {
            var selectionText = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(prompt)
                .PageSize(10)
                .AddChoices(options));
            return options.IndexOf(selectionText);
        }

        static void errorBox(string prompt)
        {
            var panel = new Panel(prompt)
                    .Header("[red]Error[/]");
            AnsiConsole.Clear();
            AnsiConsole.Write(panel);
        }

        static void EffectuateTransaction(Transaction transaction, IRepository<Account, int> acr, IRepository<Transaction, int> transr)
        {
            if (transaction.targetAccount != null)
            {
                float transferAmt = transaction.amount * transaction.rate;
                transaction.targetAccount.amount -= transferAmt;
                acr.Update(transaction.targetAccount);
            }
            if (transaction.sourceAccount != null)
            {
                transaction.sourceAccount.amount -= transaction.amount;
                acr.Update(transaction.sourceAccount);
            }
            transr.Insert(transaction);
        }
    }
}