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


            //bool exitRunning = false;

            while (true)
            {
               

                Console.WriteLine("\nLibrary Management Application");
                Console.WriteLine("1 - Author actions");
                Console.WriteLine("2 - Book actions");
                Console.WriteLine("3 - Borrower actions");
                Console.WriteLine("4 - BorrowBook");
                Console.WriteLine("5 - ReturnBook");
                Console.WriteLine("6 - En cox borrow olunan kitabi");
                Console.WriteLine("7 - Kitabi gecikdiren Borrowerlerin");
                Console.WriteLine("8 - Hansi borrower indiye qeder hansi kitablari borrow edib onlar gelsin");
                Console.WriteLine("9 - FilterBooksByTitle");
                Console.WriteLine("10 - FilterBooksByAuthor");
                Console.WriteLine("0 - Exit");

                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                     AuthorMenu(authorService);
                        break;
                    case "2":
                        BookMenu(bookService);
                        break;

                    case "3":
                        BorrowerMenu(borrowerService);
                        break;
                    case "4":
                        BorrowBook(loanService, bookService, borrowerService);
                        break;
                    case "5":
                        Console.Write("Enter Borrower ID: ");
                        int borrowerId = int.Parse(Console.ReadLine());
                        ReturnBook(loanService, borrowerId);
                        break;
                    case "6":
                        MaxBorrowedBook(loanService);
                        break;
                    case "7":
                        OverdueBorrowers(loanService);
                        break;
                    case "8":
                        BorrowersWithBooks(loanService);
                        break;
                    case "9":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();
                        PrintBooks(FilterBooksByTitle(bookService, title));
                        break;
                        case "10":
                        Console.Write("Enter author name: ");
                        string authorName = Console.ReadLine();
                        PrintBooks(FilterBooksByAuthor(bookService, authorName));
                        break;
                    case "0":
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;

                }
            }


        }

        static void AuthorMenu(IAuthorService authorService)
        {

            bool exitRunning = false;
            while (!exitRunning)
            {
                Console.WriteLine("\nAuthor Actions:");
                Console.WriteLine("1 - Butun authorlarin siyahisi");
                Console.WriteLine("2 - Author yaratmaq");
                Console.WriteLine("3 - Author editlemek");
                Console.WriteLine("4 - Author silmek");
                Console.WriteLine("0 - Back");

                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        authorService.GetAll();
                        break;

                    case "2":
                        Console.Write("Enter author name: ");
                        authorService.Create(new Author { Name = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4), CreatedAt = DateTime.UtcNow.AddHours(4) });
                        break;

                    case "3":
                        Console.Write("Enter author ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("New name: ");
                        authorService.Update(id, new Author { Name = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4) });
                        break;

                    case "4":
                        Console.Write("Enter author ID: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        authorService.Delete(deleteId);
                        Console.WriteLine($"\n{deleteId} is deleted");
                        break;
                    case "0":
                        exitRunning = true;
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void BookMenu(IBookService bookService)
        {

            bool exitRunning = false;
            while (!exitRunning)
            {
                Console.WriteLine("\nBooks Actions:");
                Console.WriteLine("1 - Butun booklarin siyahisi");
                Console.WriteLine("2 - Book yaratmaq");
                Console.WriteLine("3 - Book editlemek");
                Console.WriteLine("4 - Book silmek");
                Console.WriteLine("0 - Back");

                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bookService.GetAll();
                        break;
                    case "2":
                        Console.Write("Enter book name: ");
                        bookService.Create(new Book { Title = Console.ReadLine(), Description = Console.ReadLine(), PublishedYear = int.Parse(Console.ReadLine()), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4), CreatedAt = DateTime.UtcNow.AddHours(4) });
                        break;
                    case "3":
                        Console.Write("Enter book ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("New title: ");
                        bookService.Update(id, new Book { Title = Console.ReadLine(), PublishedYear = int.Parse(Console.ReadLine()), Description = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4) });
                        break;
                    case "4":
                        Console.Write("Enter book ID: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        bookService.Delete(deleteId);
                        Console.WriteLine($"\n{deleteId} is deleted");
                        break;
                    case "0":
                        exitRunning = true;
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;



                }
            }
        }

                static void BorrowerMenu(IBorrowerService borrowerService)
                {
                    bool exitRunning = false;
                    while (!exitRunning)
                    {
                        Console.WriteLine("\nBorrower Actions:");
                        Console.WriteLine("1 - Butun Borrowerlarin siyahisi");
                        Console.WriteLine("2 - Borrower yaratmaq");
                        Console.WriteLine("3 - Borrower editlemek");
                        Console.WriteLine("4 - Borrower silmek");
                        Console.WriteLine("0 - Back");

                        Console.Write("Choose an option: ");

                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                borrowerService.GetAll();
                                break;
                            case "2":
                                Console.Write("Enter Borrower name: ");
                                borrowerService.Create(new Borrower { Name = Console.ReadLine(), Email = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4), CreatedAt = DateTime.UtcNow.AddHours(4) });
                                break;
                            case "3":
                                Console.Write("Enter Borrower ID: ");
                                int id = int.Parse(Console.ReadLine());
                                Console.Write("New name: ");
                                borrowerService.Update(id, new Borrower { Name = Console.ReadLine(), Email = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4) });
                                break;
                            case "4":
                                Console.Write("Enter Borrower ID: ");
                                int deleteId = int.Parse(Console.ReadLine());
                                borrowerService.Delete(deleteId);
                                Console.WriteLine($"\n{deleteId} is deleted");
                                break;
                            case "0":
                                exitRunning = true;
                                return;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                    }
                }

                //static List<Book> FilterBooksByTitle(string title)
                //{
                //    IBookService bookService = new BookService();
                //    var wantedBooks = bookService.GetAll().Where(x => x.Title.Contains(title)).ToList();
                //    return wantedBooks;
                //}


                //static List<Book> FilterBooksByAuthor(string author)
                //{
                //    IBookService bookService = new BookService();
                //    IAuthorService authorService = new AuthorService();
                //    return bookService.GetAll()
                //  .Where(b => b.Authors.Any(a => a.Name == author))
                //   .ToList();
                //}


                static List<Book> FilterBooksByTitle(IBookService bookService, string title)
                {
                    return bookService.GetAll()
                        .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                static List<Book> FilterBooksByAuthor(IBookService bookService, string authorName)
                {
                    return bookService.GetAll()
                        .Where(b => b.Authors.Any(a => a.Name.Contains(authorName, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }



                static void PrintBooks(List<Book> books)
                {
                    if (books.Count == 0)
                    {
                        Console.WriteLine("No books found.");
                        return;
                    }

                    Console.WriteLine("\nFiltered Books:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"- {book.Title} (Published: {book.PublishedYear})");
                    }
                }

                static void ReturnBook(ILoanService loanService, int borrowerId)
                {
                    var activeLoans = loanService.GetAll()
                        .Where(x => x.BorrowerId == borrowerId && x.ReturnDate == null)
                        .ToList();

                    if (activeLoans.Count == 0)
                    {
                        throw new EntityNotFoundException("No active loans found for Borrower ID {borrowerId}.");
                    }

                    //foreach (var loan in activeLoans)
                    //{
                    //    loan.ReturnDate = DateTime.UtcNow.AddHours(4);
                    //    loan.UpdatedAt = DateTime.UtcNow.AddHours(4);
                    //    loanService.Update(loan.Id, loan);
                    //}

                    Console.WriteLine($"Successfully returned {activeLoans.Count} book(s) for Borrower ID {borrowerId}.");
                }

                static void MaxBorrowedBook(ILoanService loanService)
                {
                    var loans = loanService.GetAll();

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

                    var book = mostBorrowedBook.First().Book;
                    int borrowCount = mostBorrowedBook.Count();

                    Console.WriteLine($"Most borrowed book: {book.Title} (Borrowed {borrowCount} times)");
                }


                static void OverdueBorrowers(ILoanService loanService)
                {
                    DateTime now = DateTime.UtcNow.AddHours(4);

                    var overdueLoans = loanService.GetAll()
                        .Where(x => x.MustReturnDate < now && x.ReturnDate == null)
                        .ToList();

                    if (!overdueLoans.Any())
                    {
                        Console.WriteLine("No overdue loans found.");
                        return;
                    }

                    var overdueBorrowers = overdueLoans
                        .GroupBy(x => x.BorrowerId)
                        .Select(g => new
                        {
                            g.First().Borrower,
                            OverdueCount = g.Count()
                        })
                        .ToList();

                    Console.WriteLine("Overdue borrowers:");
                    foreach (var entry in overdueBorrowers)
                    {
                        if (entry.Borrower != null)
                        {
                            Console.WriteLine($"- {entry.Borrower.Name} (Overdue {entry.OverdueCount} times)");
                        }
                    }
                }

                //static void BorrowerWithBooks(ILoanService loanService)
                //{
                //    var loans = loanService.GetAll();

                //    var borrowers = loans
                //        .Where(x => x.ReturnDate == null)
                //        .GroupBy(x => x.BorrowerId)
                //        .Select(g => new
                //        {
                //            Borrower = g.First().Borrower,
                //            BookCount = g.SelectMany(x => x.LoanItems).Count()
                //        })
                //        .OrderByDescending(x => x.BookCount)
                //        .ToList();

                //    if (borrowers.Count == 0)
                //    {
                //        Console.WriteLine("No active loans found.");
                //        return;
                //    }

                //    Console.WriteLine("Borrowers with active loans:");
                //    foreach (var entry in borrowers)
                //    {
                //        if (entry.Borrower != null)
                //        {
                //            Console.WriteLine($"- {entry.Borrower.Name} (Borrowed {entry.BookCount} books)");
                //        }
                //    }


                //}


                static void BorrowersWithBooks(ILoanService loanService)
                {
                    var loans = loanService.GetAll()
                        .Where(x => x.ReturnDate != null) 
                        .GroupBy(x => x.BorrowerId)
                        .Select(g => new
                        {
                            g.First().Borrower, 
                            BorrowedBooks = g.SelectMany(x => x.LoanItems) 
                                            .Select(x => x.Book.Title) 
                                            .Distinct() 
                                            .ToList()
                        })
                        .ToList();

                    Console.WriteLine("Borrowers and their borrowed books:");
                    foreach (var entry in loans)
                    {
                        if (entry.Borrower != null && entry.BorrowedBooks.Any())
                        {
                            Console.WriteLine($"\n{entry.Borrower.Name} borrowed:");
                            foreach (var book in entry.BorrowedBooks)
                            {
                                Console.WriteLine($"- {book}");
                            }
                        }
                    }
                }

                static void BorrowBook(ILoanService loanService, IBookService bookService, IBorrowerService borrowerService)
                {
                    List<LoanItem> selectedBooks = new List<LoanItem>();
                    Borrower selectedBorrower = null;

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Available books:");

                        var books = bookService.GetAll();
                        var unavailableBookIds = loanService.GetAll()
                            .Where(x => x.ReturnDate == null) 
                            .SelectMany(x => x.LoanItems.Select(li => li.BookId))
                            .ToHashSet(); 

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
                    }

                    if (!selectedBooks.Any())
                    {
                        Console.WriteLine("No books selected. Operation canceled.");
                        return;
                    }

                    while (selectedBorrower == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Select a Borrower:");

                        var borrowers = borrowerService.GetAll();
                        foreach (var borrower in borrowers)
                        {
                            Console.WriteLine($"[{borrower.Id}] {borrower.Name}");
                        }

                        Console.Write("\nEnter Borrower ID: ");
                        if (int.TryParse(Console.ReadLine(), out int borrowerId))
                        {
                            selectedBorrower = borrowers.FirstOrDefault(b => b.Id == borrowerId);
                        }
                    }

                   
                    Console.WriteLine("\nConfirm loan? (Y/N)");
                    if (Console.ReadLine()?.Trim().ToLower() != "y")
                    {
                        Console.WriteLine("Loan canceled.");
                        return;
                    }

                    
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



            }




        }
        
    


