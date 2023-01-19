﻿using Spectre.Console;
using Expenses_Tracker___Grupo_02;

public class Transaction
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Account { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

class Program
{
    private static List<Transaction> _listTransaction = new List<Transaction>();
    private static List<string> _listAccount = new List<string>();
    private static List<string> _listCategory = new List<string>();

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMainMenu();

        }
    }
    private static void DisplayMainMenu()
    {
        Console.Clear();
        var tableTitle = new Table();
        var option = "";
        var rule = new Rule("Menu");
        tableTitle.AddColumn("Intec - Expense Tracker").Centered();
        tableTitle.Expand();
        tableTitle.Columns[0].Centered();

        AnsiConsole.Write(tableTitle);
        rule.Centered();
        AnsiConsole.Write(rule);
        Console.WriteLine();


        option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
            "New item.", "View items.", "Edit items.", "Delete items.", "Help", "Exit"
                }));
        switch (option)
        {
            //NEW ITEM
            case "New item.":
                NewItems();
                break;
            case "View items.":
                ViewItems();
                break;
            case "Edit items.":
                EditItems();
                break;
            case "Delete items.":
                DeleteItems();
                break;
            case "Help":
                Help();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
    private static void NewItems()
    {
        Console.Clear();

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "New transaction.", "New account.", "New category.", "Back", "Exit"
        }));
        switch (option)
        {
            case "New transaction.":
                NewTransaction();
                break;
            case "New account.":
                NewAccount();
                break;
            case "New category.":
                NewCategory();
                break;
            case "Back":
                return;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }

    }
    private static void NewTransaction()
    {
        CRUDs aux = new CRUDs();
        Console.Write("Enter the name of the transaction: ");
        var nameTransaction = Console.ReadLine();
        if (String.IsNullOrEmpty(nameTransaction))
        {
            Console.WriteLine("You must fill the form.");
        }
        Console.Write("\nType [Expense/Income]: ");
        var type = Console.ReadLine();
        var account = "";
        var category = "";
        if (_listAccount.Count == 0)
        {
            Console.WriteLine("\nIt looks like you haven't created an account type yet");
            Console.Write("What kind of account is it? ");
            account = Console.ReadLine();
            aux.create(_listAccount, account);

        }
        else
        {
            var accountAux = new SelectionPrompt<string>();
            foreach (var accounts in _listAccount)
            {
                accountAux.AddChoice(accounts);
            }
            accountAux.AddChoice("Add new account");

            account = AnsiConsole.Prompt(accountAux);

            if (account == "Add new account")
            {
                Console.Write("\nWhat kind of account is it?");
                account = Console.ReadLine();
                aux.create(_listAccount, account);
            }
        }

        Console.Write("\nType of account [Savings/Current/etc...]: " + account.ToString() + "\n");

        if (_listCategory.Count == 0)
        {
            Console.WriteLine("\nIt looks like you haven't created a category type yet");
            Console.Write("What kind of category is it? ");
            category = Console.ReadLine();
            aux.create(_listCategory, category);

        }
        else
        {
            var categoryAux = new SelectionPrompt<string>();
            foreach (var categories in _listCategory)
            {
                categoryAux.AddChoice(categories);
            }
            categoryAux.AddChoice("Add new category");

            category = AnsiConsole.Prompt(categoryAux);

            if (category == "Add new category")
            {
                Console.Write("\nWhat kind of category is it?");
                category = Console.ReadLine();
                aux.create(_listCategory, category);
            }
        }


        Console.Write("\nCategory: " + category + "\n");
        Console.Write("\nAmount: ");
        var amount = Console.ReadLine();
        Console.Write("\nDescription: ");
        var description = Console.ReadLine();
        string dateTime = DateTime.Now.ToString();
        Console.Write("\n");

        var newTransaction = new Transaction
        {
            Name = nameTransaction,
            Type = type,
            Account = account,
            Category = category,
            Amount = decimal.Parse(amount),
            Description = description,
            Date = DateTime.Now
        };
        _listTransaction.Add(newTransaction);

        var tableNewTransaction = new Table();
        tableNewTransaction.AddColumn(nameTransaction);
        tableNewTransaction.AddColumn("X");
        tableNewTransaction.AddRow("Type", type);
        tableNewTransaction.AddRow("Type of account", account);
        tableNewTransaction.AddRow("Category", category);
        tableNewTransaction.AddRow("Amount", amount);
        tableNewTransaction.AddRow("Description", description);
        tableNewTransaction.AddRow("Date / Time", dateTime);
        AnsiConsole.Write(tableNewTransaction);

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Back", "Exit"
                }));

        switch (option)
        {
            case "Back":
                Console.Clear();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
        }

        Console.WriteLine("Transaction added successfully.");
    }

    private static void NewAccount()
    {
        CRUDs aux = new CRUDs();
        Console.Write("What kind of account is it? ");
        var newAccount = Console.ReadLine();
        aux.create(_listAccount, newAccount);

        Console.WriteLine("Account created.\n");
        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .AddChoices(new[] {
                            "Back", "Exit"
            }));
        switch (option)
        {
            case "Back":
                Console.Clear();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
        }
        Console.WriteLine("Account added successfully.");
    }

    private static void NewCategory()
    {
        CRUDs aux = new CRUDs();
        Console.Write("What kind of category is it? ");
        var newCategory = Console.ReadLine();
        aux.create(_listCategory, newCategory);

        Console.WriteLine("Category created.\n");
        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .AddChoices(new[] {
                            "Back", "Exit"
            }));
        switch (option)
        {
            case "Back":
                Console.Clear();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
        }
    }

    private static void ViewItems()
    {
        CRUDs aux = new CRUDs();
        var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                        "View transactions.", "View accounts.", "View categories.", "Back", "Exit"
                        }));
        switch (option)
        {
            case "View transactions.":
                var tableTransactions = new Table();
                tableTransactions.AddColumns("Name", "Type", "Account", "Category", "Amount", "Description", "Date / Time");

                if (_listTransaction.Count == 0)
                {
                    Console.WriteLine("No hay transacciones registradas.");

                }
                else
                {
                    Console.WriteLine("Transacciones registradas: ");

                    foreach (var transaction in _listTransaction)
                    {
                        tableTransactions.AddRow(new[] {transaction.Name, transaction.Type, transaction.Account,
                            transaction.Category, transaction.Amount.ToString(),
                            transaction.Description, transaction.Date.ToString() });
                    }
                }

                AnsiConsole.Write(tableTransactions);
                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                            "Back", "Exit"
                        }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;

            case "View accounts.":
                aux.read(_listAccount);
                Console.WriteLine("\n");
                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                        "Back", "Exit"
                    }));
                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;

            case "View categories.":
                aux.read(_listCategory);
                Console.WriteLine("\n");
                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                        "Back", "Exit"
                    }));
                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;
            case "Back":
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                break;
        }
        Console.WriteLine("Transaction Viewed successfully.");
    }
    private static void EditItems()
    {
        CRUDs aux = new CRUDs();

        var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                            "Edit transactions.", "Edit accounts.", "Edit categories.", "Back", "Exit"
                        }));

        switch (option)
        {
            case "Edit transactions.":

                Console.WriteLine("Select the transactions you want to edit.");
                var editedTransaction = new MultiSelectionPrompt<string>().NotRequired();
                foreach (var transactions in _listTransaction)
                {
                    editedTransaction.AddChoice(transactions.Name);
                }

                var editedTransactions = AnsiConsole.Prompt(editedTransaction);
                foreach (var transactions in editedTransactions)
                {
                    var transactionToEdit = _listTransaction.FirstOrDefault(t => t.Name == transactions);
                    var editOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                        .AddChoices(new[] { "Name", "Type", "Account", "Category", "Amount", "Description" }));
                    switch (editOption)
                    {
                        case "Name":
                            Console.WriteLine("Enter the new name for the transaction: ");
                            var newName = Console.ReadLine();
                            transactionToEdit.Name = newName;
                            break;
                        case "Type":
                            Console.WriteLine("Enter the new type for the transaction: ");
                            var newType = Console.ReadLine();
                            transactionToEdit.Type = newType;
                            break;
                        case "Account":
                            Console.WriteLine("Enter the new account for the transaction: ");
                            var newAccounts = Console.ReadLine();
                            int index = _listAccount.IndexOf(transactionToEdit.Account);
                            if (index != -1)
                            {
                                _listAccount[index] = newAccounts;
                                transactionToEdit.Account = newAccounts;
                            }
                            else
                            {
                                Console.WriteLine("La cuenta no se encuentra en la lista de cuentas");
                            }
                            break;
                        case "Category":
                            Console.WriteLine("Enter the new category for the transaction: ");
                            var newCategorys = Console.ReadLine();
                            index = _listCategory.IndexOf(transactionToEdit.Category);
                            if (index != -1)
                            {
                                _listCategory[index] = newCategorys;
                                transactionToEdit.Account = newCategorys;
                            }
                            else
                            {
                                Console.WriteLine("La cuenta no se encuentra en la lista de categorias");
                            }
                            break;
                        case "Amount":
                            Console.WriteLine("Enter the new amount for the transaction: ");
                            var newAmount = decimal.Parse(Console.ReadLine());
                            transactionToEdit.Amount = newAmount;
                            break;
                        case "Description":
                            Console.WriteLine("Enter the new description for the transaction: ");
                            var newDescription = Console.ReadLine();
                            transactionToEdit.Description = newDescription;
                            break;
                    }
                    Console.WriteLine("Transaction edited successfully");
                }
                Console.WriteLine($"You edited those transactions.");

                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices(new[] {
                        "Back", "Exit"
                }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;

            case "Edit accounts.":
                foreach (string accounts in _listAccount)
                {
                    Console.Write(accounts);
                }
                Console.Write("Which account would you like to change? ");
                var oldAccount = Console.ReadLine();
                Console.Write("To which? ");
                var newAccount = Console.ReadLine();
                aux.edit(_listAccount, oldAccount, newAccount);

                Console.WriteLine("Changes made.");
                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                            "Back", "Exit"
                    }));
                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;
            case "Edit categories.":
                foreach (string category in _listCategory)
                {
                    Console.Write(category);
                }
                Console.Write("\nWhich category would you like to change? ");
                var oldCategory = Console.ReadLine();
                Console.Write("To which? ");
                var newCategory = Console.ReadLine();
                aux.edit(_listCategory, oldCategory, newCategory);

                Console.WriteLine("Changes made.");
                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                            "Back", "Exit"
                    }));
                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;
            case "Back":
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }

    private static void DeleteItems()
    {
        CRUDs aux = new CRUDs();

        var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "Delete transactions.", "Delete accounts.", "Delete categories.", "Back", "Exit"
                        }));

        switch (option)
        {
            case "Delete transactions.":
                Console.WriteLine("Select the transactions you want to remove.");
                var deletedTransaction = new MultiSelectionPrompt<string>().NotRequired();
                foreach (var transactions in _listTransaction)
                {
                    deletedTransaction.AddChoice(transactions.Name);
                }

                var deletedTransactions = AnsiConsole.Prompt(deletedTransaction);
                foreach (string transactions in deletedTransactions)
                {
                    _listTransaction.RemoveAll(t => t.Name == transactions);
                }

                Console.WriteLine($"You delete those transactions.");

                option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices(new[] {
                        "Back", "Exit"
                }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;

            case "Delete accounts.":
                Console.WriteLine("Select the accounts you want to remove.");
                var deletedAccount = new MultiSelectionPrompt<string>().NotRequired();
                foreach (var accounts in _listAccount)
                {
                    deletedAccount.AddChoice(accounts);
                }

                var deletedAccounts = AnsiConsole.Prompt(deletedAccount);

                foreach (string accounts in deletedAccounts)
                {
                    aux.delete(_listAccount, accounts);
                }

                Console.WriteLine($"You delete those accounts.");

                option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Back", "Exit"
                }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;
            case "Delete categories.":
                Console.WriteLine("Select the categories you want to remove.");
                var deletedCategory = new MultiSelectionPrompt<string>().NotRequired();
                foreach (var categories in _listCategory)
                {
                    deletedCategory.AddChoice(categories);
                }

                var deletedCategories = AnsiConsole.Prompt(deletedCategory);

                foreach (string categories in deletedCategories)
                {
                    aux.delete(_listCategory, categories);
                }

                Console.WriteLine($"You delete those categories.");

                option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Back", "Exit"
                }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
                break;
            case "Back":
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                break;
        }
        Console.WriteLine("Transaction deleted successfully.");
    }
    private static void Help()
    {
        Console.WriteLine("Intec - Expense Tracker will help you keep track of your money. \n" +
                            "With simple and intuitive graphics you can check the progress of expenses.");
        Console.ReadKey();
    }
}