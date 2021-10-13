using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.ViewModels;
using E_Loan.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanManagerServices and injecting in ManagerController constructor
        /// </summary>
        private readonly ILoanManagerServices _managerServices;
        public ManagerController(ILoanManagerServices loanManagerServices)
        {
            _managerServices = loanManagerServices;
        }
        /// <summary>
        /// Get all application details for manager
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LoanMaster>> GetAllApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Accept loan application and add remark on that, using this end point loan status is changed to "Accept"
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("accept/{loanId}/{remark}")]
        public async Task<LoanMaster> AcceptLoanApplication(int loanId, string remark)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reject loan application and add remark on that, using this end point loan status is changed to "Reject"
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reject/{loanId}/{remark}")]
        public async Task<ActionResult<LoanMaster>> RejectLoanApplication(int loanId, string remark)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Start the loan Sanction if all status and checked is passed.
        ///Before Sanctioned the loan for loanApplication Id - 
        ///Make sure Loan Applcation status is in "Accept" mode.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sanctioned-loan/{loanId}")]
        public async Task<ActionResult<LoanApprovaltrans>> SanctionedLoan([FromBody] LoanApprovalViewModel model, int loanId)
        {
            //do code here
            throw new NotImplementedException();
        }
    }
}
