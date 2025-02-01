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

        public List<Book> GetAll()
        {
            return _context.Books.Include(x=>x.Authors).ToList();
        }

        public Book? GetById(int id)
        {
            var data = _context.Books.Include(x => x.Authors).FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
