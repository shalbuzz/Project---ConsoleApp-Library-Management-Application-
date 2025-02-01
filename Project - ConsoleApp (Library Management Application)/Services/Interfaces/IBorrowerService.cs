using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces
{
    public interface IBorrowerService
    {
        void Create(Borrower borrow);
        void Update(int id, Borrower borrow);
        void Delete(int id);
        Borrower GetById(int id);
        List<Borrower> GetAll();

    }
}
