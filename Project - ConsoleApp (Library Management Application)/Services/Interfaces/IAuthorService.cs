using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Models;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces
{
    public interface IAuthorService
    {
        void Create(Author author);
        void Update(int id, Author author);
        void Delete(int id);
        Author GetById(int id);
        List<Author> GetAll();
        Author? GetByIdAuthorsInclude(int id);
        List<Author> GetAllAuthorsByInclude();
        void DeleteById(int id);
        Author GetByIdInclude(int id);
    }
}
