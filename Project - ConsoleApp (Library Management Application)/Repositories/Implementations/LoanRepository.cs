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
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository()
        {
            _context = new AppDbContext();
        }

        public List<Loan> GetAllLoan()
        {
            return _context.Loans.Include(x => x.Borrower).Include(x => x.LoanItems).ThenInclude(x=>x.Book).Where(x => !x.IsDeleted).ToList();
        }

        public List<Loan> GetAllLoanBorrower()
        {
            return _context.Loans.Include(x => x.Borrower).ToList();
        }


        public Loan? GetByIdLoan(int id)
        {
            return _context.Loans
                .Include(x => x.Borrower)
                .Include(x => x.LoanItems)
                    .ThenInclude(l => l.Book) 
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public void Attach(Loan loan)
        {
            if (_context.Entry(loan).State == EntityState.Detached)
            {
                _context.Loans.Attach(loan);  
            }
            _context.Entry(loan).State = EntityState.Modified;  
        }

        public List<Loan> GetLoansByBorrower(int borrowerId)
        {
            try
            {
               
                var loans = _context.Loans
                    .Where(l => l.BorrowerId == borrowerId && !l.IsDeleted)
                   
                    .ToList(); 

                return loans;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public void MarkAsDeleted(Loan loan)
        {
            loan.IsDeleted = true;
            loan.ReturnDate = DateTime.UtcNow;
            _context.Entry(loan).State = EntityState.Modified;
            _context.SaveChanges();
        }



    }
}
