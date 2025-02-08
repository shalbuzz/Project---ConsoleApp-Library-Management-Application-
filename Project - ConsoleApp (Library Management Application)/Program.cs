using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
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
            ILoanService loanService2 = new LoanService();
            ILoanService loanService3 = new LoanService();
            ILoanService loanService4 = new LoanService();
            ILoanService loanService5 = new LoanService();
            ILoanItemService loanItemService = new LoanItemService();


            Console.WriteLine("-------------------------------------------");

            while (true)
            {
                try
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n📚 Library Management Application");
                    Console.WriteLine("1️ - 🖊️ Author actions");
                    Console.WriteLine("2️ - 📖 Book actions");
                    Console.WriteLine("3️ - 🧑‍💼 Borrower actions");
                    Console.WriteLine("4️ - 📥 Borrow Book");
                    Console.WriteLine("5️ - 📤 Return Book");
                    Console.WriteLine("6️ - ⭐ Most Borrowed Book");
                    Console.WriteLine("7️ - ⏳ Overdue Borrowers");
                    Console.WriteLine("8️ - 🏷️ Borrowers and Their Books");
                    Console.WriteLine("9️- 🔍 Filter Books by Title");
                    Console.WriteLine("10 - 🔍 Filter Books by Author");
                    Console.WriteLine("0️ - 🚪 Exit");



                    Console.Write("\n🖊️ Choose an option: ");
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
                                BookMenu(bookService, authorService);
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
                                int borrowerId = -1; 
                                bool isValidId = false;

                               
                                while (!isValidId)
                                {
                                    Console.Write("Enter Borrower ID: ");
                                    if (int.TryParse(Console.ReadLine(), out borrowerId) && borrowerId > 0)
                                    {
                                        isValidId = true;  
                                        Returnbook(loanService, borrowerId);  
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid numeric Borrower ID greater than 0.");
                                    }
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
                                string title = string.Empty;
                                bool isValidTitle = false;

                                while (!isValidTitle)
                                {
                                    Console.Write("📚 Enter book title: ");
                                    title = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrWhiteSpace(title))
                                    {
                                        Console.WriteLine("Invalid input. Title cannot be empty.");
                                    }
                                    else
                                    {
                                        isValidTitle = true;  
                                    }
                                }

                                var filteredBooks = FilterBooksByTitle(bookService, title);
                                if (filteredBooks == null || filteredBooks.Count == 0)
                                {
                                    Console.WriteLine($"No books found with title '{title}'.");
                                }
                                else
                                {
                                    PrintBooks(filteredBooks);
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
                                string authorName = string.Empty;
                                bool isValidAuthor = false;

                              
                                while (!isValidAuthor)
                                {
                                    Console.Write("📚 Enter author name: ");
                                    authorName = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrWhiteSpace(authorName))
                                    {
                                        Console.WriteLine("Invalid input. Author name cannot be empty.");
                                    }
                                    else
                                    {
                                        isValidAuthor = true;  
                                    }
                                }

                                var filteredBooks = FilterBooksByAuthor(bookService, authorName);
                                if (filteredBooks.Count == 0)
                                {
                                    Console.WriteLine($"No books found for author '{authorName}'.");
                                }
                                else
                                {
                                    PrintBooks(filteredBooks);
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
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n🖊️ Author Actions:");
                    Console.WriteLine("1️ - 📜 List all authors");
                    Console.WriteLine("2️  - ✍️ Create an author");
                    Console.WriteLine("3️  - 📝 Edit an author");
                    Console.WriteLine("4️  - ❌ Delete an author");
                    Console.WriteLine("0️  - 🔙 Back");

                    Console.Write("🖊️ Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var authors = authorService.GetAll();
                                if (authors.Any())
                                {
                                    Console.WriteLine("\n📖 Author List:");
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
                                string? name;
                                while (true)
                                {
                                    Console.Write("🖊️ Enter author name: ");
                                    name = Console.ReadLine()?.Trim();

                                    if (!string.IsNullOrEmpty(name) && Regex.IsMatch(name, @"^[A-Za-zА-Яа-я\s]{3,}$"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid name! The name must contain only letters and spaces (minimum 3 characters). No digits or special symbols allowed.");
                                    }
                                }

                                var words = name.Split(' ');
                                for (int i = 0; i < words.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(words[i]))
                                    {
                                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                                    }
                                }
                                name = string.Join(" ", words);

                                authorService.Create(new Author
                                {
                                    Name = name,
                                    IsDeleted = false,
                                    CreatedAt = DateTime.UtcNow.AddHours(4),
                                    UpdatedAt = DateTime.UtcNow.AddHours(4),
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
                                int id;

                                while (true)
                                {
                                    Console.Write("🖊️ Enter author ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid ID format. Please enter a valid ID.");
                                    }
                                }

                                string? newName;

                                while (true)
                                {
                                    Console.Write("🖊️ New name: ");
                                    newName = Console.ReadLine()?.Trim();

                                    if (!string.IsNullOrEmpty(newName) && Regex.IsMatch(newName, @"^[A-Za-zА-Яа-я\s]{3,}$"))
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid name! The name must contain only letters and spaces (minimum 3 characters). No digits or special symbols allowed.");
                                    }
                                }

                                var words = newName.Split(' ');
                                for (int i = 0; i < words.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(words[i]))
                                    {
                                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                                    }
                                }
                                newName = string.Join(" ", words);

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
                                int deleteId = -1;
                                bool isValid = false;

                                while (!isValid)
                                {
                                    Console.Write("🖊️ Enter author ID: ");
                                    if (int.TryParse(Console.ReadLine(), out deleteId) && deleteId > 0)
                                    {
                                        var authorDelete = authorService.GetByIdInclude(deleteId);
                                        if (authorDelete != null)
                                        {
                                            isValid = true; 
                                        }
                                        else
                                        {
                                            Console.WriteLine($"❌ Author with ID {deleteId} not found. Try again.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("❌ Invalid input. Please enter a valid positive numeric ID.");
                                    }
                                }

                                authorService.DeleteById(deleteId);
                                Console.WriteLine($"✅ Author with ID {deleteId} deleted successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Unexpected error: {ex.Message}");
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

        static void BookMenu(IBookService bookService, IAuthorService authorService)
        {
            bool exitRunning = false;
            while (!exitRunning)
            {
                try
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n📖 Book Actions:");
                    Console.WriteLine("1️ - 📜 List all books");
                    Console.WriteLine("2️ - 📚 Create a book");
                    Console.WriteLine("3️ - 📝 Edit a book");
                    Console.WriteLine("4️ - ❌ Delete a book");
                    Console.WriteLine("0️ - 🔙 Back");


                    Console.Write("📖 Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var books = bookService.GetAll();
                                if (books.Any())
                                {
                                    Console.WriteLine("\n📖 Books List:");
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
                            while (true)
                            {
                                try
                                {
                                    string title;
                                    while (true)
                                    {
                                        Console.Write("📖 Enter book title: ");
                                        title = Console.ReadLine()?.Trim();

                                        if (string.IsNullOrEmpty(title) || !Regex.IsMatch(title, @"^[A-Za-zА-Яа-я0-9\s]{3,}$"))
                                        {
                                            Console.WriteLine("Invalid title! The title must contain only letters, numbers, and spaces (minimum 3 characters).");
                                            continue; 
                                        }

                                       
                                        var words = title.Split(' ');
                                        for (int i = 0; i < words.Length; i++)
                                        {
                                            if (!string.IsNullOrEmpty(words[i]))
                                            {
                                                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                                            }
                                        }
                                        title = string.Join(" ", words);
                                        break; 
                                    }

                                    string description;
                                    while (true)
                                    {
                                        Console.Write("📖 Enter book description: ");
                                        description = Console.ReadLine()?.Trim();

                                        if (string.IsNullOrEmpty(description) || !Regex.IsMatch(description, @"^[A-Za-zА-Яа-я][A-Za-zА-Яа-я0-9\s]{9,}$"))
                                        {
                                            Console.WriteLine("Invalid description! The description must start with a letter, have at least 10 characters, and contain only letters, numbers, and spaces.");
                                            continue; 
                                        }

                                        var word = description.Split(' ');
                                        for (int i = 0; i < word.Length; i++)
                                        {
                                            if (!string.IsNullOrEmpty(word[i]))
                                            {
                                                word[i] = char.ToUpper(word[i][0]) + word[i].Substring(1).ToLower();
                                            }
                                        }
                                        description = string.Join(" ", word);
                                        break; 
                                    }

                                    int publishedYear;
                                    while (true)
                                    {
                                        Console.Write("📖 Enter published year: ");
                                        string? inputYear = Console.ReadLine()?.Trim();

                                        if (string.IsNullOrEmpty(inputYear) || inputYear.Length != 4 || !int.TryParse(inputYear, out publishedYear))
                                        {
                                            Console.WriteLine("Invalid year format. The year must be a 4-digit number.");
                                            continue; 
                                        }

                                        int currentYear = DateTime.Now.Year;

                                        if (publishedYear > currentYear)
                                        {
                                            Console.WriteLine($"The published year cannot be greater than the current year ({currentYear}).");
                                            continue; 
                                        }
                                        break; 
                                    }

                                    var allAuthors = authorService.GetAll();
                                    if (!allAuthors.Any())
                                    {
                                        throw new Exception("No authors available. Please create authors first.");
                                    }

                                    Console.WriteLine("\n📖 Available Authors:");
                                    foreach (var auth in allAuthors)
                                    {
                                        Console.WriteLine($"- {auth.Id}: {auth.Name}");
                                    }

                                    int authorId;
                                  
                                    while (true)
                                    {
                                        Console.Write("📖 Enter the author's ID (or type 'exit' to cancel): ");
                                        string input = Console.ReadLine()?.Trim();

                                       
                                        if (input?.ToLower() == "exit")
                                        {
                                            Console.WriteLine("Operation cancelled.");
                                            break;  
                                        }

                                        if (!int.TryParse(input, out authorId) || authorId <= 0)
                                        {
                                            Console.WriteLine("Invalid author ID. It must be a positive integer.");
                                            continue; 
                                        }

                                        
                                        var author = authorService.GetById(authorId);
                                        if (author == null)
                                        {
                                            Console.WriteLine($"Author with ID {authorId} not found.");
                                            return; 
                                        }

                                        var newBook = new Book
                                        {
                                            Title = title,
                                            Description = description,
                                            PublishedYear = publishedYear,
                                            IsDeleted = false,
                                            CreatedAt = DateTime.UtcNow.AddHours(4),
                                            UpdatedAt = DateTime.UtcNow.AddHours(4),
                                            Authors = new List<Author> { author } 
                                        };

                                      
                                        bookService.CreateWithAuthorId(authorId, newBook);

                                        Console.WriteLine("✅ Book created successfully.");
                                        break; 
                                    }

                                    Console.Write("Do you want to add another book? (y/n): ");
                                    string? answer = Console.ReadLine()?.Trim().ToLower();
                                    if (answer != "y")
                                    {
                                        Console.WriteLine("Exiting book creation process.");
                                        break; 
                                    }
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
                            }
                            break;

                        case "3":
                            try
                            {
                                 int id;

                                while (true)
                                {
                                    Console.Write("📖 Enter book ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid ID format. Please enter a valid integer ID.");
                                    }
                                }

                               
                                string newTitle;
                                while (true)
                                {
                                    Console.Write("📖 New title: ");
                                    newTitle = Console.ReadLine()?.Trim();
                                    if (!string.IsNullOrEmpty(newTitle) && Regex.IsMatch(newTitle, @"^[A-Za-zА-Яа-я0-9]+(?: [A-Za-zА-Яа-я0-9]+)*$"))
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid title! The title must contain only letters, numbers, and spaces (minimum 3 characters per word). No special symbols allowed.");
                                    }
                                }

                               
                                var words = newTitle.Split(' ');
                                for (int i = 0; i < words.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(words[i]))
                                    {
                                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                                    }
                                }
                                newTitle = string.Join(" ", words);

                                
                                string newDescription;
                                while (true)
                                {
                                    Console.Write("📖 New description: ");
                                    newDescription = Console.ReadLine()?.Trim();
                                    if (!string.IsNullOrEmpty(newDescription) && Regex.IsMatch(newDescription, @"^[A-Za-zА-Яа-я0-9]+(?: [A-Za-zА-Яа-я0-9]+)*$") && newDescription.Length >= 10)
                                    {
                                        break; 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid description! The description must contain only letters and numbers (minimum 10 characters) and no special symbols.");
                                    }
                                }

                                
                                var word = newDescription.Split(' ');
                                for (int i = 0; i < word.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(word[i]))
                                    {
                                        word[i] = char.ToUpper(word[i][0]) + word[i].Substring(1).ToLower();
                                    }
                                }
                                newDescription = string.Join(" ", word);

                              
                                int newPublishedYear;
                                while (true)
                                {
                                    Console.Write("📖 New published year: ");
                                    string? inputYear = Console.ReadLine()?.Trim();

                                    if (Regex.IsMatch(inputYear, @"^\d{4}$") && int.TryParse(inputYear, out newPublishedYear))
                                    {
                                        int currentYear = DateTime.Now.Year;
                                        if (newPublishedYear >= 1500 && newPublishedYear <= currentYear)
                                        {
                                            break; 
                                        }
                                        else
                                        {
                                            Console.WriteLine($"The published year must be between 1500 and {currentYear}.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid year format. The year must be a 4-digit number.");
                                    }
                                }

                               
                                if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newDescription))
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
                                int deleteId;
                                while (true)
                                {
                                    Console.Write("📖 Enter book ID: ");
                                    if (int.TryParse(Console.ReadLine(), out deleteId))
                                    {
                                        var bookToDelete = bookService.GetByIdInclude(deleteId);
                                        if (bookToDelete != null)
                                        {
                                            break; 
                                        }
                                        else
                                        {
                                            Console.WriteLine($"No book found with ID {deleteId}. Please try again.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid ID format. Please enter a valid integer ID.");
                                    }
                                }

                                
                                bookService.DeleteById(deleteId);
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
                    Console.WriteLine($"Unexpected error: {ex.Message}");
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

                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n🧑‍💼 Borrower Actions:");
                    Console.WriteLine("1️ - 📜 List all borrowers");
                    Console.WriteLine("2️ - ➕ Create a borrower");
                    Console.WriteLine("3️ - 📝 Edit a borrower");
                    Console.WriteLine("4️ - ❌ Delete a borrower");
                    Console.WriteLine("0️ - 🔙 Back");


                    Console.Write("🧑‍💼 Choose an option: ");
                    string choice = Console.ReadLine()?.Trim();

                    switch (choice)
                    {
                        case "1":
                            try
                            {
                                var borrowers = borrowerService.GetAll();
                                if (borrowers.Any())
                                {
                                    Console.WriteLine("\n🧑‍💼Borrower List:");
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
                                string? name;
                                while (true)
                                {
                                    Console.Write("🧑‍💼 Enter borrower name: ");
                                    name = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[A-Za-zА-Яа-я\s.'-]{3,}$"))
                                    {
                                        Console.WriteLine("Invalid name! The name must contain only letters, spaces, dots (.), apostrophes ('), and hyphens (-), with at least 3 characters.");
                                        continue; 
                                    }

                                    var word = name.Split(' ');
                                    for (int i = 0; i < word.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(word[i]))
                                        {
                                            word[i] = char.ToUpper(word[i][0]) + word[i].Substring(1).ToLower();
                                        }
                                    }
                                    name = string.Join(" ", word);
                                    break; 
                                }

                                string? email;
                                while (true)
                                {
                                    Console.Write("🧑‍💼 Enter borrower email: ");
                                    email = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                                    {
                                        Console.WriteLine("Invalid email! The email must be in a valid format (e.g., example@mail.com).");
                                        continue; 
                                    }

                                    break; 
                                }

                                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
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
                                int id;

                                while (true)
                                {
                                    Console.Write("🧑‍💼 Enter borrower ID: ");

                                    if (!int.TryParse(Console.ReadLine(), out id))
                                    {
                                        Console.WriteLine("Invalid ID format. Please enter a valid number.");
                                        continue; 
                                    }

                                   
                                    if (id <= 0)
                                    {
                                        Console.WriteLine("ID must be a positive number.");
                                        continue; 
                                    }

                                    if (!borrowerService.GetAll().Any(x => x.Id == id))
                                    {
                                        Console.WriteLine("Borrower with this ID does not exist.");
                                        continue; 
                                    }

                                    break; 
                                }


                                
                                string? newName;

                                while (true)
                                {
                                    Console.Write("🧑‍💼 New name: ");
                                    newName = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrWhiteSpace(newName) || !Regex.IsMatch(newName, @"^[A-Za-zА-Яа-я\s.'-]{3,}$"))
                                    {
                                        Console.WriteLine("Invalid name! The name must contain only letters, spaces, dots (.), apostrophes ('), and hyphens (-), with at least 3 characters.");
                                        continue; 
                                    }

                                    var word = newName.Split(' ');
                                    for (int i = 0; i < word.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(word[i]))
                                        {
                                            word[i] = char.ToUpper(word[i][0]) + word[i].Substring(1).ToLower();
                                        }
                                    }
                                    newName = string.Join(" ", word);
                                    break; 
                                }

                                
                                string? newEmail;
                                while (true)
                                {
                                    Console.Write("🧑‍💼 New email: ");
                                    newEmail = Console.ReadLine()?.Trim();

                                    if (string.IsNullOrEmpty(newEmail) || !Regex.IsMatch(newEmail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                                    {
                                        Console.WriteLine("Invalid email! The email must be in a valid format (e.g., example@mail.com).");
                                        continue; 
                                    }
                                    break; 
                                }

                               
                                if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newEmail))
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
                                    int deleteId = -1;
                                    bool isValid = false;

                                    
                                    while (!isValid)
                                    {
                                        Console.Write("🧑‍💼 Enter borrower ID (>= 0): ");
                                        string input = Console.ReadLine();

                                       
                                        if (int.TryParse(input, out deleteId))
                                        {
                                            if (deleteId >= 0)
                                            {
                                               
                                                isValid = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("ID must be greater than or equal to 0. Please enter a valid number.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID format. Please enter a valid number.");
                                        }
                                    }

                                  
                                    borrowerService.SoftDeleteBorrower(deleteId);
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
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }


        static List<Book> FilterBooksByTitle(IBookService bookService, string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new ArgumentNullException(nameof(title), "Title cannot be null or empty.");
                }

                //var books = bookService.GetAllByTitle();
                var books = bookService.GetAll();
                if (books == null || books.Count == 0)
                {
                    Console.WriteLine("No books found in the database.");
                    return new List<Book>();
                }

          var filteredBooks = books
             .Where(b=>  /*!b.IsDeleted &&*/
                         !string.IsNullOrEmpty(b.Title) &&
                         b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
             .ToList();


                if (filteredBooks.Count == 0)
                {
                    Console.WriteLine($"No books found with title containing '{title}'.");
                }

                return filteredBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while filtering books by title: {ex.Message}");
                return new List<Book>();
            }
        }

       static List<Book> FilterBooksByAuthor(IBookService bookService, string authorName)
        {

         try

        {

        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new ArgumentNullException(nameof(authorName), "Author name cannot be null or empty.");
        }

        var books = bookService.GetAllByInclude(); 

        if (books == null || books.Count == 0)
        {
            Console.WriteLine("⚠ No books found in the database.");
            return new List<Book>();
        }

              var filteredBooks = books
                    .Where(b => !b.IsDeleted && 
                               b.Authors != null &&
                               b.Authors.Any(a => a.Name.Contains(authorName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (filteredBooks.Count == 0)
        {
            Console.WriteLine($"🚫 No books found for author '{authorName}'.");
        }

        return filteredBooks;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error while filtering books by author: {ex.Message}");
        return new List<Book>();
    }
}


        static void PrintBooks(List<Book> books)
        {
            try
            {
                if (books == null || books.Count == 0)
                {
                    Console.WriteLine("No books found.");
                    return;
                }

                Console.WriteLine("\nFiltered Books:");
                foreach (var book in books)
                {
                    if (book == null)
                    {
                        Console.WriteLine("A book entry is null and cannot be printed.");
                        continue;
                    }

                    Console.WriteLine($"- {book.Title} (Published: {book.PublishedYear})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while printing books: {ex.Message}");
            }
        }

        static void Returnbook(ILoanService loanservice, int borrowerid)
        {
            try
            {
                var activeloans = loanservice.GetAllLoan()
                    .Where(x => x.BorrowerId == borrowerid && x.ReturnDate == null && !x.IsDeleted)
                    .ToList();

                if (activeloans.Count == 0)
                {
                    throw new EntityNotFoundException($"no active loans found for borrower id {borrowerid}.");
                }


                foreach (var loan in activeloans)
                {
                    loan.ReturnDate = DateTime.UtcNow.AddHours(4);
                    loan.UpdatedAt = DateTime.UtcNow.AddHours(4);
                    loanservice.Update(loan.Id, loan);

                }

                Console.WriteLine($"successfully returned {activeloans.Count} book(s) for borrower id {borrowerid}.");
            }
            catch (EntityNotFoundException ex)
            {

                Console.WriteLine($"error: {ex.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"an error occurred while returning books: {ex.Message}");
            }
        }
    
        static void MaxBorrowedBook(ILoanService loanService)
        {
            try
            {
                var loans = loanService.GetAllLoan();

                if (loans == null || !loans.Any())
                {
                    Console.WriteLine("No loans found.");
                    return;
                }

                
                var allLoanItems = loans.SelectMany(x => x.LoanItems).ToList();
                if (allLoanItems == null || !allLoanItems.Any())
                {
                    Console.WriteLine("No books have been borrowed yet.");
                    return;
                }

                var mostBorrowedBookGroup = allLoanItems
                    .GroupBy(x => x.BookId)
                    .Where(g => g.FirstOrDefault()?.Book?.IsDeleted != true)
                    .OrderByDescending(x => x.Count())
                    .FirstOrDefault();

                if (mostBorrowedBookGroup == null)
                {
                    Console.WriteLine("No books have been borrowed yet.");
                    return;
                }

               
                var firstLoanItem = mostBorrowedBookGroup.FirstOrDefault();
                if (firstLoanItem == null || firstLoanItem.Book == null)
                {
                    Console.WriteLine("Error: The most borrowed book data is invalid.");
                    return;
                }

                Console.WriteLine($"Most borrowed book: {firstLoanItem.Book.Title} (Borrowed {mostBorrowedBookGroup.Count()} times)");
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

                var overdueLoans = loanService.GetAllLoan()
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
                    .Where(b => b.Borrower != null && !b.Borrower.IsDeleted)
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
                var loans = loanService.GetAllLoan()
                    ?.Where(x => x.ReturnDate != null)
                    .GroupBy(x => x.BorrowerId)
                    .Select(g => new
                    {
                        Borrower = g.FirstOrDefault()?.Borrower,
                        BorrowedBooks = g.SelectMany(x => x.LoanItems ?? new List<LoanItem>())
                                        .Select(x => x.Book?.Title)
                                        .Where(title => !string.IsNullOrEmpty(title))
                                        .Distinct()
                                        .ToList()
                    })
                    .Where(entry => entry.Borrower != null
                                    && entry.Borrower.IsDeleted == false   
                                    && entry.BorrowedBooks.Any())
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
                HashSet<int> unavailableBookIds = new HashSet<int>();

                while (true)
                {
                    try
                    {
                        var books = bookService.GetAll() ?? new List<Book>();

                        
                        unavailableBookIds = loanService.GetAllLoan()?
                            .Where(x => x.ReturnDate == null)
                            .SelectMany(x => x.LoanItems?.Select(li => li.BookId) ?? new List<int>())
                            .ToHashSet() ?? new HashSet<int>();

                        Console.WriteLine("\nAvailable books:");
                        if (!books.Any())
                        {
                            Console.WriteLine("No books available.");
                            return;
                        }

                        foreach (var book in books)
                        {
                            if (book.IsDeleted) 
                            {
                                continue; 
                            }
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

                        var selectedBook = books.FirstOrDefault(b => b.Id == bookId && !b.IsDeleted);
                        if (selectedBook != null)
                        {
                            selectedBooks.Add(new LoanItem { BookId = selectedBook.Id });
                            unavailableBookIds.Add(selectedBook.Id);
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
                        Console.WriteLine("\nSelect a Borrower:");

                        var borrowers = borrowerService.GetAll() ?? new List<Borrower>();

                        if (!borrowers.Any())
                        {
                            Console.WriteLine("No borrowers found.");
                            return;
                        }

                        foreach (var borrower in borrowers)
                        {
                            if (borrower.IsDeleted) 
                            {
                                continue; 
                            }
                            Console.WriteLine($"[{borrower.Id}] {borrower.Name}");
                        }

                        Console.Write("\nEnter Borrower ID: ");
                        if (int.TryParse(Console.ReadLine(), out int borrowerId))
                        {
                            selectedBorrower = borrowers.FirstOrDefault(b => b.Id == borrowerId && !b.IsDeleted);
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
        
    


