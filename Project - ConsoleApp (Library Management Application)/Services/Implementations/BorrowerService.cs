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
    public class BorrowerService : IBorrowerService
    {
        public void Create(Borrower borrow)
        {
            IBorrowerRepository borrowerRepository = new BorrowerRepository();
            if(borrow == null)
            {
                throw new ArgumentNullException("Borrower object is null");
            }

            if(borrowerRepository.GetById(borrow.Id) != null)
            {
                throw new Exception("Borrower already exists");
            }

            if(borrow.Email == null)
            {
                throw new ArgumentNullException("Email is required");
            }

            if (borrow.Name == null)
            {
                throw new ArgumentNullException("First name is required");
            }

            borrow.CreatedAt = DateTime.UtcNow.AddHours(4);
            borrow.UpdatedAt = DateTime.UtcNow.AddHours(4);

            borrowerRepository.Add(borrow);
            borrowerRepository.Commit();
        }

        public void Delete(int id)
        {
            IBorrowerRepository borrowerRepository = new BorrowerRepository();
            var data = borrowerRepository.GetById(id);
            if (data == null)
            {
                throw new Exception("Borrower not found");
            }
            if (id <= 0)
            {
                throw new Exception("Invalid Id");
            }

            if(borrowerRepository.GetById(id) == null)
            {
                throw new NotValidException("Borrower does not exist");
            }

            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);

            borrowerRepository.Remove(data);
            borrowerRepository.Commit();
        }

        public List<Borrower> GetAll()
        {
            IBorrowerRepository borrowerRepository = new BorrowerRepository();
            if (borrowerRepository.GetAll() == null)
            {
                throw new Exception("No Borrower found");
            }
            if(borrowerRepository.GetAll().Count == 0)
            {
                throw new EntityNotFoundException("No Borrower found");
            }
            
            return borrowerRepository.GetAll();
        }

        public Borrower GetById(int id)
        {
            IBorrowerRepository borrowerRepository = new BorrowerRepository();
            if (borrowerRepository.GetById(id) == null)
            {
                throw new EntityNotFoundException("Borrower not found");
            }

            if (borrowerRepository.GetAll().Count == 0)
            {
                throw new EntityNotFoundException("No Borrower found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Invalid Id");
            }

            return borrowerRepository.GetById(id);

        }

        public void Update(int id, Borrower borrow)
        {
            IBorrowerRepository borrowerRepository = new BorrowerRepository();
            var data = borrowerRepository.GetById(id);
            if (data == null)
            {
                throw new EntityNotFoundException("Borrower not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Invalid Id");
            }

            if (borrow == null)
            {
                throw new ArgumentNullException("Borrower object is null");
            }

            if (borrow.Email == null)
            {
                throw new ArgumentNullException("Email is required");
            }

            if (borrow.Name == null)
            {
                throw new ArgumentNullException("First name is required");
            }

            data.Name = borrow.Name;
            data.Email = borrow.Email;
            //data.UpdatedAt = borrow.UpdatedAt;
            //data.CreatedAt = borrow.CreatedAt;
            //data.IsDeleted = borrow.IsDeleted;
            //data.Loans = borrow.Loans;

            data.UpdatedAt = DateTime.UtcNow.AddHours(4);


            borrowerRepository.Commit();

        }
    }
}
