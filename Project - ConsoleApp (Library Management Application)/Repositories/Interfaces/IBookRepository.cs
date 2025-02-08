using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Book> GetAllByInclude();
        Book? GetByIdInclude(int id);
        void CreateWithAuthorId(int authorId, Book entity);
        void DeleteById(int id);
        List<Book> GetAllByTitle();

    }
}
