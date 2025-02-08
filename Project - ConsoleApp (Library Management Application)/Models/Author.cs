using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
