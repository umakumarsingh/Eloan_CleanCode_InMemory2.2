using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanManagerRepository : ILoanManagerRepository
    {
        /// <summary>
        /// Creating referance Variable of DbContext and injecting in LoanManagerRepository constructor
        /// </summary>

        private readonly ELoanDbContext _eLoanDbContext;
        public LoanManagerRepository(ELoanDbContext eLoanDbContext)
        {
            _eLoanDbContext = eLoanDbContext;
        }
        /// <summary>
        /// Accept loan application before start the loan approval process.
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> AcceptLoanApplication(int loanId, string remark)
        {
            try
            {
                var findLoan = await _eLoanDbContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.LStatus == LoanStatus.Received)
                {
                    findLoan.LStatus = LoanStatus.Accept;
                    findLoan.ManagerRemark = remark;
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
        /// Get list of all loan Application baed on status that is belongs to "Recived"
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            try
            {
                return await _eLoanDbContext.loanMasters.OrderByDescending(x => x.LoanName).ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Reject loan application after physical review with remark, before start the loan approval process make again as "Accept".
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> RejectLoanApplication(int loanId, string remark)
        {
            try
            {
                var findLoan = await _eLoanDbContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.LStatus == LoanStatus.Received)
                {
                    findLoan.LStatus = LoanStatus.Rejected;
                    findLoan.ManagerRemark = remark;
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
        /// Start the loan Sanction if loan status is "Accept" and add the all pending amout and all terms,
        /// with makeking loan status is Done
        /// </summary>
        /// <param name="loanApprovaltrans"></param>
        /// <returns></returns>
        public async Task<LoanApprovaltrans> SanctionedLoan(int loanId, LoanApprovaltrans loanApprovaltrans)
        {
            if (loanApprovaltrans == null)
            {
                throw new ArgumentNullException(typeof(LoanApprovaltrans).Name + "Object is Null");
            }
            try
            {
                await _eLoanDbContext.loanApprovaltrans.AddAsync(loanApprovaltrans);
                await _eLoanDbContext.SaveChangesAsync();
                var findLoan = await _eLoanDbContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.LStatus == LoanStatus.Accept)
                {
                    findLoan.LStatus = LoanStatus.Done;
                    await _eLoanDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return loanApprovaltrans;
        }
        /// <summary>
        /// Using this method check the loan status is "Accepted" or not before start loan process.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> CheckLoanStatus(int loanId)
        {
            try
            {
                var findLoan = await _eLoanDbContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.LStatus == LoanStatus.Accept)
                {
                    return findLoan;
                }
                return findLoan;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
