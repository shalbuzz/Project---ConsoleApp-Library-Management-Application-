using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Models
{
    public class Borrower : BaseEntity
    {
       public string Name { get; set; }

       public string Email { get; set; }
       
       public List<Loan> Loans { get; set; }

    }
}
