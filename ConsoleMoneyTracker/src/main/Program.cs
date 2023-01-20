using ConsoleMoneyTracker.src.main.model;
using ConsoleMoneyTracker.src.main.repository;
using Spectre.Console;

namespace ConsoleMoneyTracker.src.main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "Pedro";

            InMemoryRepository<Account, int> accountsRepository = new InMemoryRepository<Account, int>();
            InMemoryRepository<Category, int> categoryRepository = new InMemoryRepository<Category, int>();
            InMemoryRepository<Currency, string> currencyRepository = new InMemoryRepository<Currency, string>();
            InMemoryRepository<Transaction, int> transactionRepository = new InMemoryRepository<Transaction, int>();

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
                    if (accountsRepository.Count() <= 0)
                    {
                        var panel = new Panel("You need at least [red]one[/] [green]account[/] to make transactions.")
                            .Header("[red]Error[/]");
                        AnsiConsole.Clear();
                        AnsiConsole.Write(panel);
                        continue;
                    }
                    if (categoryRepository.Count() <= 0)
                    {
                        var panel = new Panel("You need at least [red]one[/] [olive]category[/] to make transactions.")
                            .Header("[red]Error[/]");
                        AnsiConsole.Clear();
                        AnsiConsole.Write(panel);
                        continue;
                    }
                    var accounts = accountsRepository.GetAll().ToList();
                    var accountSelectionList = accounts.Select((it) => { return $"{it.ID}. {it.item.name}"; }).ToList();
                    accountSelectionList.Add("Income");

                    selectionText = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Where is the money coming from?")
                        .PageSize(10)
                        .AddChoices(accountSelectionList));
                    selection = accountSelectionList.IndexOf(selectionText);


                    bool isIncome = selection == accountSelectionList.Count() - 1;

                }
            }
        }
    }
}