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
    public class LoanItemService : ILoanItemService
    {
        public void Create(LoanItem loanItem)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            if (loanItemRepository == null)
            {
                throw new NotValidException("LoanItemService is not null");
            }

            if(loanItem == null)
            {
                throw new NotValidException("LoanItem is not null");
            }

            if(loanItem.Book == null)
            {
                throw new NotValidException("Book is not null");
            }

            if (loanItem.Loan == null)
            {
                throw new NotValidException("Loan is not null");
            }

            if (loanItem.Loan.LoanItems == null)
            {
                throw new NotValidException("LoanItems is not null");
            }

            if (!loanItem.Loan.LoanItems.Contains(loanItem))
            {
                throw new NotValidException ("LoanItems is not contains loanItem");
            }

            loanItem.CreatedAt = DateTime.UtcNow.AddHours(4);
            loanItem.UpdatedAt = DateTime.UtcNow.AddHours(4);



            loanItemRepository.Add(loanItem);
            loanItemRepository.Commit();


        }

        public void Delete(int id)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();

            var data = loanItemRepository.GetById(id);

            if (data == null)
            {
                throw new NotValidException("LoanItem is not found");
            }


            if (loanItemRepository == null)
            {
                throw new NotValidException("LoanItemRepository is not null");
            }

            if(id <= 0)
            {
                throw new NotValidException("Id is not valid");
            }

            if(loanItemRepository.GetById(id) == null) { 
                throw new NotValidException("LoanItem is not found");
            }

            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);

            //loanItemRepository.Remove(data);
            loanItemRepository.Commit();

        }

        public List<LoanItem> GetAll()
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            if (loanItemRepository == null)
            {
                throw new NotValidException("LoanItemRepository is not null");
            }

            if(loanItemRepository.GetAll() == null)
            {
                throw new EntityNotFoundException("LoanItem is not found");
            }

            if(loanItemRepository.GetAll().Count == 0)
            {
                throw new EntityNotFoundException("LoanItem is not found");
            }



            return loanItemRepository.GetAll();
        }

        public LoanItem GetById(int id)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            if (loanItemRepository == null)
            {
                throw new NotValidException("LoanItemRepository is not null");
            }

            if (loanItemRepository.GetById(id) == null)
            {
                throw new EntityNotFoundException("LoanItem is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is not valid");
            }

            return loanItemRepository.GetById(id);
        }

        public void Update(int id, LoanItem loanItem)
        {
            ILoanItemRepository loanItemRepository = new LoanItemRepository();
            var data = loanItemRepository.GetById(id);
            if (data == null)
            {
                throw new NotValidException("LoanItem is not found");
            }

            if (loanItem == null)
            {
                throw new NotValidException("LoanItem is not null");
            }

            if (loanItem.Book == null)
            {
                throw new NotValidException("Book is not null");
            }

            if (loanItem.Loan == null)
            {
                throw new NotValidException("Loan is not null");
            }

            if (loanItem.Loan.LoanItems == null)
            {
                throw new NotValidException("LoanItems is not null");
            }

            if (!loanItem.Loan.LoanItems.Contains(loanItem))
            {
                throw new NotValidException("LoanItems is not contains loanItem");
            }
            //data.Loan = loanItem.Loan;
            //data.Book = loanItem.Book;
            //data.UpdatedAt = loanItem.UpdatedAt;
            //data.CreatedAt = loanItem.CreatedAt;
            //data.IsDeleted = loanItem.IsDeleted;
            data.BookId = loanItem.BookId;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);

            loanItemRepository.Commit();

        }
    }
}
