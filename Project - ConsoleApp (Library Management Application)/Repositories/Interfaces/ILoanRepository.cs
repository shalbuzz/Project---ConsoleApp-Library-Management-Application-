using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        List<Loan> GetAllLoan();
        Loan? GetByIdLoan(int id);
        List<Loan> GetAllLoanBorrower();
        void Attach(Loan loan);
        List<Loan> GetLoansByBorrower(int borrowerId);
        void MarkAsDeleted(Loan loan);
    }
}
