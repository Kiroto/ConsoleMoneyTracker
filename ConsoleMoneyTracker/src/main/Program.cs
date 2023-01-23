using ConsoleMoneyTracker.src.main.controller;
using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using Spectre.Console;
using System.Reflection;
using System.Xml.Linq;

namespace ConsoleMoneyTracker.src.main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "Pedro";

            IRepository<Account, int> accountsRepository = new InMemoryRepository<Account, int>();
            IRepository<Category, int> categoryRepository = new InMemoryRepository<Category, int>();
            IRepository<Currency, string> currencyRepository = new InMemoryRepository<Currency, string>();
            IRepository<Transaction, int> transactionRepository = new InMemoryRepository<Transaction, int>();
            IRepository<ListItem, int> itemRepository = new InMemoryRepository<ListItem, int>();

            TransactionController transactionController = new TransactionController(transactionRepository, itemRepository, accountsRepository);
            AccountController accountController = new AccountController(accountsRepository, transactionRepository, itemRepository);
            CategoryController categoryController = new CategoryController(categoryRepository, itemRepository);
            CurrencyController currencyController = new CurrencyController(currencyRepository, new OnlineCurrencyInfoGetter());
            List<string> list = new List<string>()
                {
                    "Add Transaction", "Manage Information", "See a Report", "Exit",
                };


            while (true)
            {
                try
                {

                    int selection = SelectOption(list, $"Welcome [blue]{userName}[/]! What would you like to do?");
                    AnsiConsole.Clear();
                    if (selection == 0)
                    {
                        MakeTransactionPipeline(transactionController, accountController, categoryController);
                    }
                    else if (selection == 1)
                    {
                        ManageInformationScreen(accountController, categoryController, currencyController, transactionController);
                    }
                    else if (selection == list.Count() - 1)
                    {
                        break;
                    }
                }
                catch (NotImplementedException e)
                {
                    ShowErrorBox("This feature has not been implemented yet.");
                }
            }
        }

        // Make a transaction given the information in the database
        // TODO: update currencies before making transferences
        static void MakeTransactionPipeline(TransactionController transControl, AccountController actControl, CategoryController catControl)
        {
            int accountCount = actControl.Count();
            // Validate if a transaction is possible
            if (accountCount <= 0)
            {
                ShowErrorBox("You need at least [red]one[/] [green]account[/] to make transactions.");
                return;
            }
            if (catControl.Count() <= 0)
            {
                ShowErrorBox("You need at least [red]one[/] [olive]category[/] to make transactions.");
                return;
            }

            // Select the kind of transaction
            int transactionType = SelectOption(new List<string>() { "Expense", "Income", "Movement" }, "What kind of transaction?");

            // Select the accounts
            Account? sourceAccount = null;
            Account? targetAccount = null;
            List<Account> selectableAccounts = actControl.GetAccounts().ToList();

            switch (transactionType)
            {
                case 0:
                    sourceAccount = SelectListable(selectableAccounts, "Select the source of the expense");
                    break;
                case 1:

                    targetAccount = SelectListable(selectableAccounts, "Select the target of the income.");
                    break;
                case 2:
                    if (accountCount < 2)
                    {
                        ShowErrorBox("You need at least [red]two[/] [olive]accounts[/] to make movements.");
                        return;
                    }
                    sourceAccount = SelectListable(selectableAccounts, "Select the source of the movement.");
                    selectableAccounts.Remove(sourceAccount);
                    targetAccount = SelectListable(selectableAccounts, "Select the target of the movement.");
                    // get both
                    break;
                default:
                    // error
                    break;
            }

            // Get the amount to transfer
            float amount = AnsiConsole.Ask<float>($"How much shall be transferred?{(sourceAccount != null ? "(Available: [blue]" + sourceAccount.amount + "[/]" : " ")}");
            if (sourceAccount != null && amount > sourceAccount.amount)
            {
                ShowErrorBox("There isn't enough balance on this account for this transference");
                return;
            }

            List<Category> selectableCategories = catControl.GetCategories().ToList();

            Category category = SelectListable(selectableCategories, "Select the category of this transaction.");
            string description = AnsiConsole.Ask<string>("Write a [green]description[/] for this transaction.");

            Transaction transaction = transControl.MakeTransaction(sourceAccount, targetAccount, amount, category, description);

            ShowTransactionSummary(transaction);

            if (AnsiConsole.Confirm("Are these OK?"))
            {
                transControl.CommitTransaction(transaction);
            };
        }

        #region AllCRUD
        // Manage Accounts
        // Manage Categories
        // Manage Currencies
        // See Transactions
        static void ManageInformationScreen(AccountController accountController, CategoryController categoryController, CurrencyController currencyController, TransactionController transactionController)
        {
            List<string> options = new List<string>()
            {
                "Manage Accounts",
                "Manage Categories",
                "Manage Currencies",
                "See Transactions",
                "Go Back"
            };

            while (true)
            {
                int selected = SelectOption(options, "What do you wish to do?");

                switch (selected)
                {
                    case 0:
                        ManageAccountsScreen(accountController);
                        break;
                    case 1:
                        ManageCategoriesScreen();
                        break;
                    case 2:
                        ManageCurrenciesScreen(currencyController);
                        break;
                    case 3:
                        SeeTransactionsScreen(transactionController);
                        break;
                    case 4:
                        return; // Goes back to the previous menu
                }
            }
        }

        #region AccountCRUD

        // CRUD Accounts
        static void ManageAccountsScreen(AccountController accountCtrl)
        {
            List<string> options = new List<string>()
            {
                "Create Account",
                "Read Accounts",
                "Update Account",
                "Delete Account",
                "Go Back"
            };

            while (true)
            {
                int selected = SelectOption(options, "What do you wish to do?");

                switch (selected)
                {
                    case 0:
                        CreateAccountScreen(accountCtrl);
                        break;
                    case 1:
                        ReadAccountsScreen(accountCtrl);
                        break;
                    case 2:
                        UpdateAccountScreen(accountCtrl);
                        break;
                    case 3:
                        DeleteAccountScreen(accountCtrl);
                        break;
                    case 4:
                        return; // Goes back to the previous menu
                }
            }
        }

        static void CreateAccountScreen(AccountController accountCtrl)
        {
            throw new NotImplementedException();
        }

        static void ReadAccountsScreen(AccountController accountCtrl)
        {
            var accounts = accountCtrl.GetAccounts().ToList();
            ShowListableTable(accounts, "Press Enter to Exit.");
        }

        static void UpdateAccountScreen(AccountController accountCtrl)
        {
            throw new NotImplementedException();
        }

        static void DeleteAccountScreen(AccountController accountCtrl)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Categories
        // CRUD Categories
        static void ManageCategoriesScreen()
        {
            List<string> options = new List<string>()
            {
                "Create Category",
                "Read Categories",
                "Update Category",
                "Delete Category",
                "Go Back"
            };

            while (true)
            {
                int selected = SelectOption(options, "What do you wish to do?");

                switch (selected)
                {
                    case 0:
                        CreateCategoryScreen();
                        break;
                    case 1:
                        ReadCategoriesScreen();
                        break;
                    case 2:
                        UpdateCategoryScreen();
                        break;
                    case 3:
                        DeleteCategoryScreen();
                        break;
                    case 4:
                        return; // Goes back to the previous menu
                }
            }
        }

        static void CreateCategoryScreen()
        {
            throw new NotImplementedException();
        }

        static void ReadCategoriesScreen()
        {
            throw new NotImplementedException();
        }

        static void UpdateCategoryScreen()
        {
            throw new NotImplementedException();
        }

        static void DeleteCategoryScreen()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Currencies
        // CRUD Currencies
        static void ManageCurrenciesScreen(CurrencyController currencyController)
        {
            List<string> options = new List<string>()
            {
                "Read Currencies",
                "Update Currencies from web",
                "Go Back"
            };

            while (true)
            {
                int selected = SelectOption(options, "What do you wish to do?");

                switch (selected)
                {
                    case 0:
                        ReadCurrenciesScreen(currencyController);
                        break;
                    case 1:
                        UpdateCurrenciesScreen(currencyController);
                        break;
                    case 2:
                        return; // Goes back to the previous menu
                }
            }
        }

        static void ReadCurrenciesScreen(CurrencyController currencyController)
        {
            var currencies = currencyController.GetCurrencyList().ToList();
            ShowStrListableTable(currencies, "Press Enter to Exit.");
        }

        static void UpdateCurrenciesScreen(CurrencyController currencyController)
        {
            var t = currencyController.updateCurrenciesFromInfoGetter();
            string waitingString = ".";
            while (!t.IsCompleted)
            {
                AnsiConsole.Clear();
                ShowBox($"Getting Currencies {waitingString}");
                Thread.Sleep(200);
                waitingString += ".";
            }
            if (t.IsCompletedSuccessfully)
            {
                ShowBox("Updated Currency Database!");
            }
        }

        #endregion
        static void SeeTransactionsScreen(TransactionController transCtrl)
        {
            var transactions = transCtrl.GetTransactions().ToList();
            ShowListableTable(transactions, "Press Enter to Exit.");
        }

        // TODO: Make it a table
        static void ShowStrListableTable<T>(IList<T> listable, string prompt) where T : IListable, IIndexable<string>
        {
            var listing = listable.Select((it) => { return $"{it.ID} - {(it.item.shortName != null ? it.item.shortName : "").PadRight(3)} {it.item.name} {it.item.description}"; }).ToList();
            if (listing.Count == 0)
            {
                listing.Add("There are no items");
            }
            int selectedIndex = SelectOption(listing, prompt);
        }

        static void ShowListableTable<T>(IList<T> listable, string prompt) where T : IListable, IIndexable<int>
        {
            var listing = listable.Select((it) => { return $"{it.ID} {it.item.name}: {it.item.description}"; }).ToList();
            if (listing.Count == 0)
            {
                listing.Add("There are no items");
            }
            int selectedIndex = SelectOption(listing, prompt);
        }

        #endregion

        #region Utilities
        static T SelectListable<T>(IList<T> listable, string prompt) where T : IListable, IIndexable<int>
        {
            var listing = listable.Select((it) => { return $"{it.ID}. {it.item.name}"; }).ToList();
            int selectedIndex = SelectOption(listing, prompt);

            return listable[selectedIndex];
        }

        static void ShowTransactionSummary(Transaction transaction)
        {
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
        }

        static int SelectOption(IList<string> options, string prompt)
        {
            var selectionText = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(prompt)
                .PageSize(10)
                .AddChoices(options));
            return options.IndexOf(selectionText);
        }

        static void ShowErrorBox(string prompt)
        {
            var panel = new Panel(prompt)
                    .Header("[red]Error[/]");
            AnsiConsole.Clear();
            AnsiConsole.Write(panel);
        }

        static void ShowBox(string prompt)
        {
            var panel = new Panel(prompt)
                    .Header("[blue]Processing[/]");
            AnsiConsole.Clear();
            AnsiConsole.Write(panel);
        }

        #endregion
    }
}