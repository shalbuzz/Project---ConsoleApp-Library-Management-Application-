using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository() 
        {
            _context = new AppDbContext();
        }

        public List<Book> GetAllByInclude()
        {
            return _context.Books.Include(x=>x.Authors).Where(x => !x.IsDeleted).ToList();
        }


        public List<Book> GetAllByTitle()
        {
            return _context.Books.Where((x => !x.IsDeleted)).ToList();
        }
        public void CreateWithAuthorId(int authorId, Book entity)
        {


            Book book = new Book()
            {
                Title = entity.Title,
                Description = entity.Description,
                PublishedYear = entity.PublishedYear,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4),
                Authors = new List<Author>() { _context.Authors.FirstOrDefault(x => x.Id == authorId) }
            };
            _context.Books.Add(book);
            _context.SaveChanges();

        }
        public Book? GetByIdInclude(int id)
        {
            var data = _context.Books.Include(x => x.Authors).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public void DeleteById(int id)
        {
            var book = _context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            // Удаляем связи книги с авторами в промежуточной таблице
            book.Authors.Clear();

            // Помечаем книгу как удаленную
            book.IsDeleted = true;
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();
        }

    }
}
