using E_Loan.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Interfaces
{
    public interface ILoanManagerServices
    {
        Task<IEnumerable<LoanMaster>> AllLoanApplication();
        Task<LoanMaster> AcceptLoanApplication(int loanId, string remark);
        Task<LoanMaster> RejectLoanApplication(int loanId, string remark);
        Task<LoanApprovaltrans> SanctionedLoan(int loanId, LoanApprovaltrans loanApprovaltrans);
        Task<LoanMaster> CheckLoanStatus(int loanId);
    }
}
