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

        public List<LoanItem> GetAllLoanItem()
        {
            return _context.LoanItems.Include(x=>x.Loan).Include(x => x.Book).Where(x => !x.IsDeleted).ToList();

        }

        public LoanItem GetByIdLoanItem(int id)
        {
            var data = _context.LoanItems.Include(x => x.Loan).Include(x => x.Book).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public List<LoanItem> GetLoanItemsByBorrower(int borrowerId)
        {
            try
            {
                var loanItems = _context.LoanItems
                    .Where(li => li.Loan.BorrowerId == borrowerId && !li.IsDeleted)
                    .ToList(); 

                return loanItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public void MarkAsDeleted(LoanItem loanItem)
        {
            loanItem.IsDeleted = true;
            _context.Entry(loanItem).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
