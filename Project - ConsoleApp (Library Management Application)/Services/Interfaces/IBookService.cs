using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces
{
    public interface IBookService
    {
        void Create(Book book);
        void Update(int id, Book book);
        void Delete(int id);
        Book GetById(int id);
        List<Book> GetAll();

    }
}
