using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanManagerServices : ILoanManagerServices
    {
        /// <summary>
        /// Creating ILoanManagerRepository field/object and injecting into LoanManagerServices constructor
        /// </summary>
        private readonly ILoanManagerRepository _managerRepository;
        public LoanManagerServices(ILoanManagerRepository loanManagerRepository)
        {
            _managerRepository = loanManagerRepository;
        }
        /// <summary>
        /// Accept loan application by manager before start the laon approval process
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> AcceptLoanApplication(int loanId, string remark)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get all Loan Application for loan Manager
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Check loan status before starting the loan process
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> CheckLoanStatus(int loanId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reject loan application with remark if any details missing and not valid
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> RejectLoanApplication(int loanId, string remark)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// After all verifaction is done loan Sanction is start and add all other details.
        /// Make loan status Done after loan is finnaly approved
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="loanApprovaltrans"></param>
        /// <returns></returns>
        public async Task<LoanApprovaltrans> SanctionedLoan(int loanId, LoanApprovaltrans loanApprovaltrans)
        {
            //do code here
            throw new NotImplementedException();
        }
    }
}
