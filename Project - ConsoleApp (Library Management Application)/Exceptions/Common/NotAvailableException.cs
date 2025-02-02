using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Exceptions.Common
{
    public class NotAvailableException : Exception
    {
        public NotAvailableException(string message) : base(message)
        {
        }

        public NotAvailableException()
        {
            
        }
    }
}
