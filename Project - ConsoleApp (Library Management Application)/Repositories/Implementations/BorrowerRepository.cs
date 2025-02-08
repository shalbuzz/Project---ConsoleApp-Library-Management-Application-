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
    public class BorrowerRepository : GenericRepository<Borrower>, IBorrowerRepository
    {
        private readonly AppDbContext _context;
        public BorrowerRepository()
        {
            _context = new AppDbContext();
        }

        public Borrower? GetBorrowerWithLoans(int id)
        {
            return _context.Borrowers
                           .Include(b => b.Loans)
                               .ThenInclude(l => l.LoanItems)  
                           .FirstOrDefault(b => b.Id == id && !b.IsDeleted);
        }

        public void MarkAsDeleted(Borrower borrower)
        {
            borrower.IsDeleted = true;
            borrower.UpdatedAt = DateTime.UtcNow;
            _context.Entry(borrower).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
