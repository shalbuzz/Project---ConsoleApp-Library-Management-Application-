using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces
{
    public interface ILoanItemService
    {
        void Create(LoanItem loanItem);
        void Update(int id, LoanItem loanItem);
        void Delete(int id);
        LoanItem GetById(int id);
        List<LoanItem> GetAll();
    }
}
