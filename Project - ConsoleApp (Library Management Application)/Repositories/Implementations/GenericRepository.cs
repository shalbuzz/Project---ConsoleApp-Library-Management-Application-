using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations
{
     
        public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
        {

            private readonly AppDbContext _context;

            public GenericRepository()
            {
                _context = new AppDbContext();
            }
            public void Add(T entity)
            {
                _context.Set<T>().Add(entity);
            }

            public int Commit()
            {
                return _context.SaveChanges();
            }

            public List<T> GetAll()
            {
                return _context.Set<T>().Where(x=>!x.IsDeleted).ToList();
            }

            public T GetById(int id)
            {
                var data = _context.Set<T>().Where(x => !x.IsDeleted).FirstOrDefault(x => x.Id == id);
                return data;
            }

            public void Remove(T entity)
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }

