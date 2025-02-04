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
    public class LoanService : ILoanService
    {
        public void Create(Loan loan)
        {

            ILoanRepository loanRepository = new LoanRepository();

            if(loanRepository is null)
            {
                throw new EntityNotFoundException(nameof(loanRepository));
            }

            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }
            if (loan.LoanItems == null)
            {
                throw new ArgumentNullException(nameof(loan.LoanItems));
            }
            if (loan.LoanItems.Count == 0)
            {
                throw new ArgumentNullException(nameof(loan.LoanItems));
            }

            loan.CreatedAt = DateTime.UtcNow.AddHours(4);
            loan.UpdatedAt = DateTime.UtcNow.AddHours(4);

            loanRepository.Add(loan);
            loanRepository.Commit();


        }

        public void Delete(int id)
        {
            ILoanRepository loanRepository = new LoanRepository();
            var data = loanRepository.GetById(id);
            if (data is null)
            {
                throw new EntityNotFoundException("Loan is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

            //loanRepository.Remove(data);
            data.IsDeleted = true;
            data.UpdatedAt = DateTime.UtcNow.AddHours(4);

            loanRepository.Commit();
        }

        public List<Loan> GetAll()
        {

            ILoanRepository loanRepository = new LoanRepository();
            if (loanRepository.GetAll() is null)
            {
                throw new EntityNotFoundException(nameof(Loan));
            }

            if (loanRepository.GetAll().Count == 0)
            {
                throw new EntityNotFoundException(nameof(Loan));
            }


            return loanRepository.GetAll();
        }

        public Loan GetById(int id)
        {
            ILoanRepository loanRepository = new LoanRepository();
            if (loanRepository.GetById(id) is null)
            {
                throw new EntityNotFoundException("Loan is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

            if (loanRepository.GetById(id) == null)
            {
                throw new NotValidException("Loan is not found");
            }

            return loanRepository.GetById(id);





        }

        public void Update(int id, Loan loan)
        {
            ILoanRepository loanRepository = new LoanRepository();
            var data = loanRepository.GetById(id);
            if (data is null)
            {
                throw new EntityNotFoundException("Loan is not found");
            }

            if (id <= 0)
            {
                throw new NotValidException("Id is invalid");
            }

            if (loan == null)
            {
                throw new NotValidException("Loan is not null");
            }

            if (loan.LoanItems == null)
            {
                throw new NotValidException("LoanItems is not null");
            }

            if (loan.LoanItems.Count == 0)
            {
                throw new NotValidException("LoanItems is not null");
            }

            //data.LoanItems = loan.LoanItems;
            //data.Borrower = loan.Borrower;
            //data.LoanDate = loan.LoanDate;
             data.ReturnDate = loan.ReturnDate;
            //data.MustReturnDate = loan.MustReturnDate;
            //data.BorrowerId = loan.BorrowerId;

            //data.CreatedAt = loan.CreatedAt;
            //data.UpdatedAt = loan.UpdatedAt;
            //data.IsDeleted = loan.IsDeleted;

            data.UpdatedAt = DateTime.UtcNow.AddHours(4);



            loanRepository.Commit();


        }
    }
}
