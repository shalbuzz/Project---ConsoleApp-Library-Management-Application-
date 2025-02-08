using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Exceptions.Common;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations
{
    public class AuthorService : IAuthorService 
    {
        public void Create(Author author)
        {
            IAuthorRepository authorRepository = new AuthorRepository();

            if(authorRepository is null)
            {
                throw new Exception("Author Repository is not initialized");
            }

            
            if (author is null)
            {
                throw new Exception("Author is not initialized");
            }

            if(author.Name is null)
            {
                throw new Exception("Author Name is not initialized");
            }

            if (string.IsNullOrWhiteSpace(author.Name))
            {
                throw new Exception("Author Name is not initialized");
            }

            author.CreatedAt = DateTime.UtcNow.AddHours(4);
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);

            authorRepository.Add(author);
            authorRepository.Commit();
        }

        public void Delete(int id)
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            var data = authorRepository.GetById(id);
            if (data is null)
            {
                throw new EntityNotFoundException("Book is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

           
            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);
            authorRepository.Commit();
        }

        public List<Author> GetAll()
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            if (authorRepository is null)
            {
                throw new Exception("Author Repository is not initialized");
            }

            if (authorRepository.GetAll() is null)
            {
                throw new EntityNotFoundException("Author is not found");
            }

            return authorRepository.GetAll();
        }


        public Author GetByIdAuthorsInclude(int id)
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            if (authorRepository is null)
            {
                throw new Exception("Author Repository is not initialized");
            }

            if (authorRepository.GetAll() is null)
            {
                throw new EntityNotFoundException("Author is not found");
            }
            return authorRepository.GetByIdAuthorsInclude(id);
        }

        public List<Author> GetAllAuthorsByInclude()
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            if (authorRepository is null)
            {
                throw new Exception("Author Repository is not initialized");
            }

            if (authorRepository.GetAllAuthorsByInclude() is null)
            {
                throw new EntityNotFoundException("Author is not found");
            }

            return authorRepository.GetAllAuthorsByInclude();
        }

        public Author GetById(int id)
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            if (authorRepository is null)
            {
                throw new Exception("Author Repository is not initialized");
            }

            if (authorRepository.GetAll() is null)
            {
                throw new EntityNotFoundException("Author is not found");
            }
          return authorRepository.GetById(id);
        }

        public void DeleteById(int id)
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            var author = authorRepository.GetByIdInclude(id);

            if (author == null)
            {
                throw new Exception("Book not found.");
            }

          
            authorRepository.DeleteById(id);
        }

        public Author GetByIdInclude(int id)
        {
       IAuthorRepository authorRepository = new AuthorRepository();   
            
            if (authorRepository.GetByIdInclude(id) is null)
            {
                throw new EntityNotFoundException("Book is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

            return authorRepository.GetByIdInclude(id);
        }

        public void Update(int id, Author author)
        {
            IAuthorRepository authorRepository = new AuthorRepository();
            var data = authorRepository.GetById(id);
            if (data is null)
            {
                throw new EntityNotFoundException("Author is not found");
            }

            if (author is null)
            {
                throw new Exception("Author is not initialized");
            }

            if (author.Name is null)
            {
                throw new Exception("Author Name is not initialized");
            }

          

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

           
            data.Name = author.Name;
            //data.Books = author.Books;
            //data.CreatedAt = author.CreatedAt;
            //data.IsDeleted = author.IsDeleted;
            
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);


            authorRepository.Commit();

        }
    }
}
