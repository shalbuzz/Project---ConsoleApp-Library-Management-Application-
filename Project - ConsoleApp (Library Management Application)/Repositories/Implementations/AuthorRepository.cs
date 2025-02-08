using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository()
        {
            _context = new AppDbContext();
        }
        public List<Author> GetAllAuthorsByInclude()
        {
            return _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted).ToList();
        }

        public Author? GetByIdAuthorsInclude(int id)
        {
            var data = _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public Author? GetByIdInclude(int id)
        {
            var data = _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public void DeleteById(int id)
        {
            var author = _context.Authors.Include(b => b.Books).FirstOrDefault(b => b.Id == id);

            if (author == null)
            {
                throw new Exception("Book not found.");
            }

            // Удаляем связи книги с авторами в промежуточной таблице
            author.Books.Clear();

            // Помечаем книгу как удаленную
            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();
        }

    }
}
