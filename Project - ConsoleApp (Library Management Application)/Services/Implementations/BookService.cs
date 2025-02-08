    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Exceptions.Common;
    using Project___ConsoleApp__Library_Management_Application_.Models;
    using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
    using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
    using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations
{
    public class BookService : IBookService
    {

        public void Create(Book book)
        {
            IBookRepository bookRepository = new BookRepository();
            IAuthorRepository authorRepository = new AuthorRepository();

            if (book is null)
            {
                throw new ArgumentNullException("Book is null");
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentNullException("Title is null or empty");
            }

            if (string.IsNullOrWhiteSpace(book.Description))
            {
                throw new EntityNotFoundException("Description is null or empty");
            }

            if (book.PublishedYear <= 0)
            {
                throw new NotValidException("Published year is invalid");
            }

            book.CreatedAt = DateTime.UtcNow.AddHours(4);
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);

            if (book.Authors != null && book.Authors.Any())
            {
                
                var authorIds = book.Authors.Select(a => a.Id).ToList();
                var authorsInDb = authorRepository.GetAllAuthorsByInclude().Where(a => authorIds.Contains(a.Id)).ToList();

                book.Authors = authorsInDb;
            }

            try
            {
                bookRepository.Add(book);
                bookRepository.Commit();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public Book GetByIdInclude(int id)
        {
            IBookRepository bookRepository = new BookRepository();
            if (bookRepository.GetByIdInclude(id) is null)
            {
                throw new EntityNotFoundException("Book is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

            return bookRepository.GetByIdInclude(id);
        }

        public void CreateWithAuthors(List<int> authorIds, Book entity)
        {
            IBookRepository bookRepository = new BookRepository();
            IAuthorRepository authorRepository = new AuthorRepository();


           
            var authors = authorRepository.GetAllAuthorsByInclude().Where(a => authorIds.Contains(a.Id)).ToList();
            if (!authors.Any())
            {
                throw new Exception("No valid authors found.");
            }

           
            entity.Authors =new List<Author>() {};
            

            
            bookRepository.Add(entity);
            bookRepository.Commit();
        }

        public void CreateWithAuthorId(int authorId, Book entity)
        {
            using var context = new AppDbContext(); 

            var author = new Author { Id = authorId }; 
            context.Authors.Attach(author); 

            entity.Authors = new List<Author> { author }; 

            context.Books.Add(entity); 
            context.SaveChanges(); 

            
        }

        public void Delete(int id)
        {
            IBookRepository bookRepository = new BookRepository();
            var data = bookRepository.GetById(id);
            if (data is null)
            {
                throw new EntityNotFoundException("Book is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }
            data.Authors.Clear(); 

            bookRepository.Commit();

           
            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);

            bookRepository.Commit();
        }

        public void DeleteById(int id)
        {
            IBookRepository bookRepository = new BookRepository();
            var book = bookRepository.GetByIdInclude(id);

            if (book == null)
            {
                throw new Exception("Book not found.");
            }

           
            bookRepository.DeleteById(id);
        }

        public List<Book> GetAll()
            {
                IBookRepository bookRepository = new BookRepository();
           
                if (bookRepository.GetAll() is null)
                {
                    throw new EntityNotFoundException("Book is not found");
                }


                return bookRepository.GetAll();


            }

        public List<Book> GetAllByTitle()
        {
            IBookRepository bookRepository = new BookRepository();

            if (bookRepository.GetAllByTitle() is null)
            {
                throw new EntityNotFoundException("Book is not found");
            }


            return bookRepository.GetAllByTitle();

        }


        public List<Book> GetAllByInclude()
            {
                IBookRepository bookRepository = new BookRepository();
                if (bookRepository.GetAllByInclude() is null)
                {
                    throw new EntityNotFoundException("Book is not found");
                }

                return bookRepository.GetAllByInclude();
            }

            public Book GetById(int id)
            {
                IBookRepository bookRepository = new BookRepository();
                if (bookRepository.GetById(id) is null)
                {
                    throw new EntityNotFoundException("Book is not found");
                }

                if(id <= 0)
                {
                    throw new NotValidException("Id is invalid");
                }

                return bookRepository.GetById(id);

            }

            public void Update(int id, Book book)
            {
                IBookRepository bookRepository = new BookRepository();
                var data = bookRepository.GetById(id);
                if (data is null)
                {
                    throw new EntityNotFoundException("Book is not found");
                }

                if (id <= 0)
                {
                    throw new NotValidException("Id is invalid");
                }

                if (book is null)
                {
                    throw new ArgumentNullException("Book is null");
                }

                if (string.IsNullOrWhiteSpace(book.Title))
                {
                    throw new ArgumentNullException("Title is null or empty");
                }

                if (string.IsNullOrWhiteSpace(book.Description))
                {
                    throw new EntityNotFoundException("Author is null or empty");
                }

                if (book.PublishedYear <= 0)
                {
                    throw new NotValidException("Published year is invalid");
                }

                data.Title = book.Title;
                data.Description = book.Description;
                data.PublishedYear = book.PublishedYear;
                //data.Authors = book.Authors;
                //data.CreatedAt = book.CreatedAt;
                //data.UpdatedAt = book.UpdatedAt;
                //data.IsDeleted = book.IsDeleted;


                data.UpdatedAt = DateTime.UtcNow.AddHours(4);

                bookRepository.Commit();
            }
        }
    }