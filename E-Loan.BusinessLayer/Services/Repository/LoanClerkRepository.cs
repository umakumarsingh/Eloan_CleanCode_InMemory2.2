using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanClerkRepository : ILoanClerkRepository
    {
        /// <summary>
        /// Creating referance Variable of DbContext and injecting in LoanClerkRepository Constructor.
        /// </summary>
        private readonly ELoanDbContext _eLoanDbContext;
        public LoanClerkRepository(ELoanDbContext eLoanDbContext)
        {
            _eLoanDbContext = eLoanDbContext;
        }
        /// <summary>
        /// Show/Get all loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            try
            {
                return await _eLoanDbContext.loanMasters
                    .OrderByDescending(x => x.LoanName).ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Show/Get all loan application that status is Not Recived
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> NotReceivedLoanApplication()
        {
            try
            {
                return await _eLoanDbContext.loanMasters
                    .Where(x => x.LStatus == LoanStatus.NotReceived).ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Start the loan process and add the remening data by loan clerk
        /// </summary>
        /// <param name="loanProcesstrans"></param>
        /// <returns></returns>
        public async Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans)
        {
            try
            {
                if (loanProcesstrans == null)
                {
                    throw new ArgumentNullException(typeof(LoanProcesstrans).Name + "Object is Null");
                }
                await _eLoanDbContext.loanProcesstrans.AddAsync(loanProcesstrans);
                await _eLoanDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            return loanProcesstrans;
        }
        /// <summary>
        /// Make the loan application as "Recived" before starting loan process using this method
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> ReceivedLoan(int loanId)
        {
            try
            {
                var findLoan = await _eLoanDbContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan != null && findLoan.LStatus == LoanStatus.NotReceived)
                {
                    findLoan.LStatus = LoanStatus.Received;
                    await _eLoanDbContext.SaveChangesAsync();
                }
                return findLoan;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Find and get all loan application that is recived for loan clerk
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> ReceivedLoanApplication()
        {
            try
            {
                var result = await _eLoanDbContext.loanMasters.
                Where(x => x.LStatus == LoanStatus.Received).Take(10).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
