using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces
{
    public interface ILoanService
    {
        void Create(Loan loan);
        void Update(int id, Loan loan);
        void Delete(int id);
        Loan GetById(int id);
        List<Loan> GetAll();
        List<Loan> GetAllLoan();
        Loan GetByIdLoan(int id);

       
    }
}
