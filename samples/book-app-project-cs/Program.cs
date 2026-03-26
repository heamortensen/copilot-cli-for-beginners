using BookApp.Models;
using BookApp.Services;

var collection = new BookCollection();

void ShowBooks(List<Book> books)
{
    if (books.Count == 0)
    {
        Console.WriteLine("No books found.");
        return;
    }

    Console.WriteLine("\nYour Book Collection:\n");

    for (int i = 0; i < books.Count; i++)
    {
        var book = books[i];
        var status = book.Read ? "✓" : " ";
        Console.WriteLine($"{i + 1}. [{status}] {book.Title} by {book.Author} ({book.Year})");
    }

    Console.WriteLine();
}

void HandleList()
{
    var books = collection.ListBooks();
    ShowBooks(books);
}

void HandleAdd()
{
    Console.WriteLine("\nAdd a New Book\n");

    Console.Write("Title: ");
    var title = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Author: ");
    var author = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Year: ");
    var yearStr = Console.ReadLine()?.Trim() ?? "";

    if (int.TryParse(yearStr, out var year))
    {
        collection.AddBook(title, author, year);
        Console.WriteLine("\nBook added successfully.\n");
    }
    else
    {
        Console.WriteLine($"\nError: '{yearStr}' is not a valid year.\n");
    }
}

void HandleRemove()
{
    Console.WriteLine("\nRemove a Book\n");

    Console.Write("Enter the title of the book to remove: ");
    var title = Console.ReadLine()?.Trim() ?? "";
    collection.RemoveBook(title);

    Console.WriteLine("\nBook removed if it existed.\n");
}

void HandleFind()
{
    Console.WriteLine("\nFind Books by Author\n");

    Console.Write("Author name: ");
    var author = Console.ReadLine()?.Trim() ?? "";
    var books = collection.FindByAuthor(author);

    ShowBooks(books);
}

void HandleMark()
{
    Console.WriteLine("\nMark a Book as Read\n");

    var books = collection.ListBooks();
    ShowBooks(books);

    if (books.Count == 0)
    {
        return;
    }

    Console.Write("Enter the number of the book to mark as read: ");
    var selectionStr = Console.ReadLine()?.Trim() ?? "";

    if (!int.TryParse(selectionStr, out var selection))
    {
        Console.WriteLine($"\nError: '{selectionStr}' is not a valid number.\n");
        return;
    }

    if (selection < 1 || selection > books.Count)
    {
        Console.WriteLine("\nError: selection is out of range.\n");
        return;
    }

    var book = books[selection - 1];

    if (book.Read)
    {
        Console.WriteLine($"\n'{book.Title}' is already marked as read.\n");
        return;
    }

    var result = collection.MarkAsRead(book.Title);

    if (result)
    {
        Console.WriteLine($"\nMarked '{book.Title}' as read.\n");
    }
    else
    {
        Console.WriteLine("\nBook not found.\n");
    }
}

void ShowHelp()
{
    Console.WriteLine("""

    Book Collection Helper

    Commands:
      list     - Show all books
      add      - Add a new book
      remove   - Remove a book by title
      find     - Find books by author
      mark     - Mark a book as read
      help     - Show this help message
    """);
}

if (args.Length == 0)
{
    ShowHelp();
    return;
}

var command = args[0].ToLower();

switch (command)
{
    case "list":
        HandleList();
        break;
    case "add":
        HandleAdd();
        break;
    case "remove":
        HandleRemove();
        break;
    case "find":
        HandleFind();
        break;
    case "mark":
        HandleMark();
        break;
    case "help":
        ShowHelp();
        break;
    default:
        Console.WriteLine("Unknown command.\n");
        ShowHelp();
        break;
}
