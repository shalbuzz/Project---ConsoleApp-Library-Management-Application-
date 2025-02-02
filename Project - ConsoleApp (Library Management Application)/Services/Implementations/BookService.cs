using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            book.CreatedAt = DateTime.UtcNow.AddHours(4);
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);

            bookRepository.Add(book);
            bookRepository.Commit();
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

            //bookRepository.Remove(data);
            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);
            bookRepository.Commit();
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