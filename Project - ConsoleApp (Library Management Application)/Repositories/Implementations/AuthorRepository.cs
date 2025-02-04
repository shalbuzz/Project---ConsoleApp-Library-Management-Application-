﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository()
        {
            _context = new AppDbContext();
        }
        public List<Author> GetAllAuthorsByInclude()
        {
            return _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted).ToList();
        }

        public Author? GetByIdAuthorsInclude(int id)
        {
            var data = _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
            return data;
        }
    }
}
