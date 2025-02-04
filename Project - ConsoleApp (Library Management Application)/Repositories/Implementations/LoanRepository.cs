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
            return _context.Loans.Include(x => x.Borrower).Where(x => !x.IsDeleted).ToList();
        }

        public Loan? GetByIdLoan(int id)
        {
            var data = _context.Loans.Include(x => x.Borrower).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
