using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanCustomerRepository : ILoanCustomerRepository
    {
        /// <summary>
        /// Creating referance Variable of EloanDbContext and Injecting in LoanCustomerRepository constructor
        /// </summary>
        private readonly ELoanDbContext _eLoanDbContext;
        public LoanCustomerRepository(ELoanDbContext eLoanDbContext)
        {
            _eLoanDbContext = eLoanDbContext;
        }
        /// <summary>
        /// Apply mortage and save all data in InMemory Database collection.
        /// </summary>
        /// <param name="loanMaster"></param>
        /// <returns></returns>
        public async Task<LoanMaster> ApplyMortgage(LoanMaster loanMaster)
        {
            if (loanMaster == null)
            {
                throw new ArgumentNullException(typeof(LoanMaster).Name + "Object is Null");
            }
            try
            {
                await _eLoanDbContext.loanMasters.AddAsync(loanMaster);
                await _eLoanDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            return loanMaster;
        }
        /// <summary>
        /// Get the loan status by id applied by customer
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> AppliedLoanStatus(int loanId)
        {
            try
            {
                var loan = await _eLoanDbContext.loanMasters.Where(x => x.LoanId == loanId).
                OrderByDescending(x => x.LoanName).FirstOrDefaultAsync();
                return loan;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Update an existing loan application before sent to loan clerk if loan status is recived update not possible
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="loanMaster"></param>
        /// <returns></returns>
        public async Task<LoanMaster> UpdateMortgage(int loanId, LoanMaster loanMaster)
        {
            if (loanMaster == null && loanId <= 0)
            {
                throw new ArgumentNullException(typeof(LoanMaster).Name + "Object is Null");
            }
            var loan = await _eLoanDbContext.loanMasters.Where(x => x.LoanId == loanId).
                OrderByDescending(x => x.LoanName).FirstOrDefaultAsync();
            if (loan.LStatus == LoanStatus.Received)
            {
                throw new ArgumentNullException(typeof(LoanMaster).Name + "Loan is Recive not able to update..");
            }
            try
            {
                _eLoanDbContext.Entry(loanMaster).State = EntityState.Detached;
                await _eLoanDbContext.SaveChangesAsync();
                return loanMaster;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }
}
