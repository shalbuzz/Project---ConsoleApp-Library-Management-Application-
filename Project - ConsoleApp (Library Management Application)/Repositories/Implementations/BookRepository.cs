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

        public Book? GetByIdInclude(int id)
        {
            var data = _context.Books.Include(x => x.Authors).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
