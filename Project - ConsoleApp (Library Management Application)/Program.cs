using Microsoft.VisualBasic;
using Project___ConsoleApp__Library_Management_Application_.Exceptions.Common;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Services.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            IAuthorService authorService = new AuthorService();
            IBorrowerService borrowerService = new BorrowerService();
            ILoanService loanService = new LoanService();
            ILoanItemService loanItemService = new LoanItemService();


          

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\n📚 Library Management Application");
                    Console.WriteLine("1 - Author actions");
                    Console.WriteLine("2 - Book actions");
                    Console.WriteLine("3 - Borrower actions");
                    Console.WriteLine("4 - Borrow Book");
                    Console.WriteLine("5 - Return Book");
                    Console.WriteLine("6 - Most Borrowed Book");
                    Console.WriteLine("7 - Overdue Borrowers");
                    Console.WriteLine("8 - Borrowers and Their Books");
                    Console.WriteLine("9 - Filter Books by Title");
                    Console.WriteLine("10 - Filter Books by Author");
                    Console.WriteLine("0 - Exit");

                    Console.Write("\nChoose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                AuthorMenu(authorService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in Author Menu: {ex.Message}");
                            }
                            break;

                        case "2":
                            try
                            {
                                BookMenu(bookService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in Book Menu: {ex.Message}");
                            }
                            break;

                        case "3":
                            try
                            {
                                BorrowerMenu(borrowerService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in Borrower Menu: {ex.Message}");
                            }
                            break;

                        case "4":
                            try
                            {
                                BorrowBook(loanService, bookService, borrowerService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while borrowing book: {ex.Message}");
                            }
                            break;

                        case "5":
                            try
                            {
                                Console.Write("Enter Borrower ID: ");
                                if (int.TryParse(Console.ReadLine(), out int borrowerId))
                                {
                                    ReturnBook(loanService, borrowerId);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid numeric Borrower ID.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while returning book: {ex.Message}");
                            }
                            break;

                        case "6":
                            try
                            {
                                MaxBorrowedBook(loanService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while fetching the most borrowed book: {ex.Message}");
                            }
                            break;

                        case "7":
                            try
                            {
                                OverdueBorrowers(loanService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while fetching overdue borrowers: {ex.Message}");
                            }
                            break;

                        case "8":
                            try
                            {
                                BorrowersWithBooks(loanService);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while fetching borrowers and their books: {ex.Message}");
                            }
                            break;

                        case "9":
                            try
                            {
                                Console.Write("Enter book title: ");
                                string? title = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrEmpty(title))
                                {
                                    PrintBooks(FilterBooksByTitle(bookService, title));
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Title cannot be empty.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while filtering books by title: {ex.Message}");
                            }
                            break;

                        case "10":
                            try
                            {
                                Console.Write("Enter author name: ");
                                string? authorName = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrEmpty(authorName))
                                {
                                    PrintBooks(FilterBooksByAuthor(bookService, authorName));
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Author name cannot be empty.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while filtering books by author: {ex.Message}");
                            }
                            break;

                        case "0":
                            Console.WriteLine("Exiting the application...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 0-10.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Critical error: {ex.Message}");
                }
            }

        }


    




        static void AuthorMenu(IAuthorService authorService)
        {
            bool exitRunning = false;
            while (!exitRunning)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nAuthor Actions:");
                    Console.WriteLine("1 - List all authors");
                    Console.WriteLine("2 - Create an author");
                    Console.WriteLine("3 - Edit an author");
                    Console.WriteLine("4 - Delete an author");
                    Console.WriteLine("0 - Back");

                    Console.Write("Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var authors = authorService.GetAll();
                                if (authors.Any())
                                {
                                    Console.WriteLine("\nAuthor List:");
                                    foreach (var author in authors)
                                    {
                                        Console.WriteLine($"- {author.Id}: {author.Name}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No authors found.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching authors: {ex.Message}");
                            }
                            break;

                        case "2":
                            try
                            {
                                Console.Write("Enter author name: ");
                                string? name = Console.ReadLine()?.Trim();

                                if (string.IsNullOrEmpty(name))
                                {
                                    throw new ArgumentException("Author name cannot be empty.");
                                }

                                authorService.Create(new Author
                                {
                                    Name = name,
                                    IsDeleted = false,
                                    UpdatedAt = DateTime.UtcNow.AddHours(4),
                                    CreatedAt = DateTime.UtcNow.AddHours(4)
                                });

                                Console.WriteLine("Author created successfully.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "3":
                            try
                            {
                                Console.Write("Enter author ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int id))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                Console.Write("New name: ");
                                string? newName = Console.ReadLine()?.Trim();

                                if (string.IsNullOrEmpty(newName))
                                {
                                    throw new ArgumentException("New name cannot be empty.");
                                }

                                authorService.Update(id, new Author
                                {
                                    Name = newName,
                                    IsDeleted = false,
                                    UpdatedAt = DateTime.UtcNow.AddHours(4)
                                });

                                Console.WriteLine("Author updated successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter author ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int deleteId))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                authorService.Delete(deleteId);
                                Console.WriteLine($"Author with ID {deleteId} deleted successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "0":
                            exitRunning = true;
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 0-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Critical error: {ex.Message}");
                }
            }
        }

    


        static void BookMenu(IBookService bookService)
        {
            bool exitRunning = false;
            while (!exitRunning)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nBooks Actions:");
                    Console.WriteLine("1 - Butun booklarin siyahisi");
                    Console.WriteLine("2 - Book yaratmaq");
                    Console.WriteLine("3 - Book editlemek");
                    Console.WriteLine("4 - Book silmek");
                    Console.WriteLine("0 - Back");

                    Console.Write("Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var books = bookService.GetAll();
                                if (books.Any())
                                {
                                    Console.WriteLine("\nBooks List:");
                                    foreach (var book in books)
                                    {
                                        Console.WriteLine($"- {book.Id}: {book.Title} ({book.PublishedYear})");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No books found.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching books: {ex.Message}");
                            }
                            break;

                        case "2":
                            try
                            {
                                Console.Write("Enter book title: ");
                                string? title = Console.ReadLine()?.Trim();

                                Console.Write("Enter book description: ");
                                string? description = Console.ReadLine()?.Trim();

                                Console.Write("Enter published year: ");
                                if (!int.TryParse(Console.ReadLine(), out int publishedYear))
                                {
                                    throw new FormatException("Invalid year format.");
                                }

                                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                                {
                                    throw new ArgumentException("Title and description cannot be empty.");
                                }

                                bookService.Create(new Book
                                {
                                    Title = title,
                                    Description = description,
                                    PublishedYear = publishedYear,
                                    IsDeleted = false,
                                    UpdatedAt = DateTime.UtcNow.AddHours(4),
                                    CreatedAt = DateTime.UtcNow.AddHours(4)
                                });

                                Console.WriteLine("Book created successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "3":
                            try
                            {
                                Console.Write("Enter book ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int id))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                Console.Write("New title: ");
                                string? newTitle = Console.ReadLine()?.Trim();

                                Console.Write("New description: ");
                                string? newDescription = Console.ReadLine()?.Trim();

                                Console.Write("New published year: ");
                                if (!int.TryParse(Console.ReadLine(), out int newPublishedYear))
                                {
                                    throw new FormatException("Invalid year format.");
                                }

                                if (string.IsNullOrEmpty(newTitle) || string.IsNullOrEmpty(newDescription))
                                {
                                    throw new ArgumentException("Title and description cannot be empty.");
                                }

                                bookService.Update(id, new Book
                                {
                                    Title = newTitle,
                                    Description = newDescription,
                                    PublishedYear = newPublishedYear,
                                    IsDeleted = false,
                                    UpdatedAt = DateTime.UtcNow.AddHours(4)
                                });

                                Console.WriteLine("Book updated successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter book ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int deleteId))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                bookService.Delete(deleteId);
                                Console.WriteLine($"Book with ID {deleteId} deleted successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "0":
                            exitRunning = true;
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 0-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Critical error: {ex.Message}");
                }
            }
        }




        static void BorrowerMenu(IBorrowerService borrowerService)
        {
            bool exitRunning = false;
            while (!exitRunning)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nBorrower Actions:");
                    Console.WriteLine("1 - List all borrowers");
                    Console.WriteLine("2 - Create a borrower");
                    Console.WriteLine("3 - Edit a borrower");
                    Console.WriteLine("4 - Delete a borrower");
                    Console.WriteLine("0 - Back");

                    Console.Write("Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var borrowers = borrowerService.GetAll();
                                if (borrowers.Any())
                                {
                                    Console.WriteLine("\nBorrower List:");
                                    foreach (var borrower in borrowers)
                                    {
                                        Console.WriteLine($"- {borrower.Id}: {borrower.Name} ({borrower.Email})");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No borrowers found.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error fetching borrowers: {ex.Message}");
                            }
                            break;

                        case "2":
                            try
                            {
                                Console.Write("Enter borrower name: ");
                                string? name = Console.ReadLine()?.Trim();

                                Console.Write("Enter borrower email: ");
                                string? email = Console.ReadLine()?.Trim();

                                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                                {
                                    throw new ArgumentException("Name and email cannot be empty.");
                                }

                                borrowerService.Create(new Borrower
                                {
                                    Name = name,
                                    Email = email
                                });

                                Console.WriteLine("Borrower created successfully.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "3":
                            try
                            {
                                Console.Write("Enter borrower ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int id))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                Console.Write("New name: ");
                                string? newName = Console.ReadLine()?.Trim();

                                Console.Write("New email: ");
                                string? newEmail = Console.ReadLine()?.Trim();

                                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newEmail))
                                {
                                    throw new ArgumentException("Name and email cannot be empty.");
                                }

                                borrowerService.Update(id, new Borrower
                                {
                                    Name = newName,
                                    Email = newEmail
                                });

                                Console.WriteLine("Borrower updated successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Validation error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter borrower ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int deleteId))
                                {
                                    throw new FormatException("Invalid ID format.");
                                }

                                borrowerService.Delete(deleteId);
                                Console.WriteLine($"Borrower with ID {deleteId} deleted successfully.");
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Input error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case "0":
                            exitRunning = true;
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 0-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Critical error: {ex.Message}");
                }
            }
        }



        static List<Book> FilterBooksByTitle(IBookService bookService, string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Error: The title cannot be null or empty.");
                    return new List<Book>(); // Возвращаем пустой список
                }

                var books = bookService.GetAll();
                if (books == null)
                {
                    Console.WriteLine("Error: Unable to retrieve book list. The data source returned null.");
                    return new List<Book>();
                }

                return books
                    .Where(b => b.Title != null && b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: The title parameter cannot be null. {ex.Message}");
                return new List<Book>(); // Возвращаем пустой список при ошибке
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while filtering books by title: {ex.Message}");
                return new List<Book>(); // Возвращаем пустой список при ошибке
            }
        }


        static List<Book> FilterBooksByAuthor(IBookService bookService, string authorName)
          {
           
            try
            {
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    Console.WriteLine("Error: The title cannot be null or empty.");
                    return new List<Book>(); // Возвращаем пустой список
                }

                return bookService.GetAll()
                    .Where(b => b.Authors.Any(a => a.Name.Contains(authorName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: The title parameter cannot be null. {ex.Message}");
                return new List<Book>(); // Возвращаем пустой список при ошибке
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while filtering books by title: {ex.Message}");
                return new List<Book>(); // Возвращаем пустой список при ошибке
            }

        }



        static void PrintBooks(List<Book> books)
        {
            try
            {
                if (books.Count == 0)
                {
                    Console.WriteLine("No books found.");
                    return;
                }

                if (books.Any(b => b == null))
                {
                    Console.WriteLine("Error: One or more book entries are null and cannot be printed.");
                    return;
                }

                Console.WriteLine("\nFiltered Books:");
                foreach (var book in books)
                {
                    if (book == null) // Проверка на случай, если книга окажется null
                    {
                        Console.WriteLine("A book entry is null and cannot be printed.");
                        continue;
                    }
                    Console.WriteLine($"- {book.Title} (Published: {book.PublishedYear})");
                }
            }
            catch (NullReferenceException nullEx)
            {
                Console.WriteLine($"A null reference error occurred: {nullEx.Message}");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine($"Argument error: {argEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing books: {ex.Message}");
            }
        }


        static void ReturnBook(ILoanService loanService, int borrowerId)
        {
            try
            {
                var activeLoans = loanService.GetAll()
                    .Where(x => x.BorrowerId == borrowerId && x.ReturnDate == null)
                    .ToList();

                if (activeLoans.Count == 0)
                {
                    throw new EntityNotFoundException($"No active loans found for Borrower ID {borrowerId}.");
                }

                // Отключаем комментарии на код, который обновляет записи
                foreach (var loan in activeLoans)
                {
                    loan.ReturnDate = DateTime.UtcNow.AddHours(4);
                    loan.UpdatedAt = DateTime.UtcNow.AddHours(4);
                    loanService.Update(loan.Id, loan);
                }

                Console.WriteLine($"Successfully returned {activeLoans.Count} book(s) for Borrower ID {borrowerId}.");
            }
            catch (EntityNotFoundException ex)
            {
                // Обрабатываем ошибку, если активные займы не найдены
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Общий блок для перехвата других ошибок
                Console.WriteLine($"An error occurred while returning books: {ex.Message}");
            }
        }

        static void MaxBorrowedBook(ILoanService loanService)
        {
            try
            {
                var loans = loanService.GetAll();

                if (loans == null || !loans.Any())
                {
                    Console.WriteLine("No loans found.");
                    return;
                }

                var mostBorrowedBook = loans
                    .SelectMany(x => x.LoanItems)
                    .GroupBy(x => x.BookId)
                    .OrderByDescending(x => x.Count())
                    .FirstOrDefault();

                if (mostBorrowedBook == null)
                {
                    Console.WriteLine("No books have been borrowed yet.");
                    return;
                }

                var book = mostBorrowedBook.FirstOrDefault()?.Book;
                int borrowCount = mostBorrowedBook.Count();
                if (book == null)
                {
                    Console.WriteLine("Error: The most borrowed book data is invalid.");
                    return;
                }

                Console.WriteLine($"Most borrowed book: {book.Title} (Borrowed {borrowCount} times)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the most borrowed book: {ex.Message}");
            }
        }


        static void OverdueBorrowers(ILoanService loanService)
        {
            try
            {
                DateTime now = DateTime.UtcNow.AddHours(4);

                var overdueLoans = loanService.GetAll()
                    ?.Where(x => x.MustReturnDate < now && x.ReturnDate == null)
                    .ToList();

                if (overdueLoans == null || !overdueLoans.Any())
                {
                    Console.WriteLine("No overdue loans found.");
                    return;
                }

                var overdueBorrowers = overdueLoans
                    .GroupBy(x => x.BorrowerId)
                    .Select(g => new
                    {
                        Borrower = g.FirstOrDefault()?.Borrower,
                        OverdueCount = g.Count()
                    })
                    .Where(b => b.Borrower != null) // Исключаем возможные null-записи
                    .ToList();

                Console.WriteLine("Overdue borrowers:");
                foreach (var entry in overdueBorrowers)
                {
                    Console.WriteLine($"- {entry.Borrower.Name} (Overdue {entry.OverdueCount} times)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving overdue borrowers: {ex.Message}");
            }
        }




        static void BorrowersWithBooks(ILoanService loanService)
        {
            try
            {
                var loans = loanService.GetAll()
                    ?.Where(x => x.ReturnDate != null)
                    .GroupBy(x => x.BorrowerId)
                    .Select(g => new
                    {
                        Borrower = g.FirstOrDefault()?.Borrower,
                        BorrowedBooks = g.SelectMany(x => x.LoanItems ?? new List<LoanItem>()) // Защита от null
                                        .Select(x => x.Book?.Title) // Проверяем, что у книги есть название
                                        .Where(title => !string.IsNullOrEmpty(title)) // Исключаем null или пустые строки
                                        .Distinct()
                                        .ToList()
                    })
                    .Where(entry => entry.Borrower != null && entry.BorrowedBooks.Any()) // Фильтруем пустые записи
                    .ToList();

                if (loans == null || loans.Count == 0)
                {
                    Console.WriteLine("No borrowers with borrowed books found.");
                    return;
                }

                Console.WriteLine("Borrowers and their borrowed books:");
                foreach (var entry in loans)
                {
                    Console.WriteLine($"\n{entry.Borrower.Name} borrowed:");
                    foreach (var book in entry.BorrowedBooks)
                    {
                        Console.WriteLine($"- {book}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving borrowers with books: {ex.Message}");
            }
        }


        static void BorrowBook(ILoanService loanService, IBookService bookService, IBorrowerService borrowerService)
        {
            try
            {
                List<LoanItem> selectedBooks = new List<LoanItem>();
                Borrower selectedBorrower = null;

                while (true)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Available books:");

                        var books = bookService.GetAll() ?? new List<Book>();
                        var unavailableBookIds = loanService.GetAll()?
                            .Where(x => x.ReturnDate == null)
                            .SelectMany(x => x.LoanItems?.Select(li => li.BookId) ?? new List<int>())
                            .ToHashSet() ?? new HashSet<int>();

                        if (!books.Any())
                        {
                            Console.WriteLine("No books available.");
                            return;
                        }

                        foreach (var book in books)
                        {
                            string status = unavailableBookIds.Contains(book.Id) ? "Not Available" : "Available";
                            Console.WriteLine($"[{book.Id}] {book.Title} - {status}");
                        }

                        Console.Write("\nEnter Book ID to borrow (or 0 to continue): ");
                        if (!int.TryParse(Console.ReadLine(), out int bookId) || bookId == 0)
                            break;

                        if (unavailableBookIds.Contains(bookId))
                        {
                            Console.WriteLine("This book is not available.");
                            continue;
                        }

                        var selectedBook = books.FirstOrDefault(b => b.Id == bookId);
                        if (selectedBook != null)
                        {
                            selectedBooks.Add(new LoanItem { BookId = selectedBook.Id });
                            Console.WriteLine($"Added: {selectedBook.Title}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Book ID. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while selecting books: {ex.Message}");
                    }
                }

                if (!selectedBooks.Any())
                {
                    Console.WriteLine("No books selected. Operation canceled.");
                    return;
                }

                while (selectedBorrower == null)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Select a Borrower:");

                        var borrowers = borrowerService.GetAll() ?? new List<Borrower>();

                        if (!borrowers.Any())
                        {
                            Console.WriteLine("No borrowers found.");
                            return;
                        }

                        foreach (var borrower in borrowers)
                        {
                            Console.WriteLine($"[{borrower.Id}] {borrower.Name}");
                        }

                        Console.Write("\nEnter Borrower ID: ");
                        if (int.TryParse(Console.ReadLine(), out int borrowerId))
                        {
                            selectedBorrower = borrowers.FirstOrDefault(b => b.Id == borrowerId);
                            if (selectedBorrower == null)
                            {
                                Console.WriteLine("Invalid Borrower ID. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Borrower ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while selecting a borrower: {ex.Message}");
                    }
                }

                Console.WriteLine("\nConfirm loan? (Y/N)");
                if (Console.ReadLine()?.Trim().ToLower() != "y")
                {
                    Console.WriteLine("Loan canceled.");
                    return;
                }

                try
                {
                    var newLoan = new Loan
                    {
                        BorrowerId = selectedBorrower.Id,
                        LoanDate = DateTime.UtcNow.AddHours(4),
                        MustReturnDate = DateTime.UtcNow.AddHours(4).AddDays(15),
                        LoanItems = selectedBooks
                    };

                    loanService.Create(newLoan);
                    Console.WriteLine("Loan successfully created!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while creating the loan: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


    }




}
        
    


