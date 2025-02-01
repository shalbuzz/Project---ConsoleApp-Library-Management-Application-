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
    public class LoanItemRepository : GenericRepository<LoanItem>, ILoanItemRepository
    {
        private readonly AppDbContext _context;

        public LoanItemRepository()
        {
            _context = new AppDbContext();
        }

        public List<LoanItem> GetAll()
        {
            return _context.LoanItems.Include(x=>x.Loan).Include(x => x.Book).ToList();

        }

        public LoanItem GetById(int id)
        {
            var data = _context.LoanItems.Include(x => x.Loan).Include(x => x.Book).FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
