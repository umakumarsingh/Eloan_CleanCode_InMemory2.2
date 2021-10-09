using E_Loan.BusinessLayer.Interfaces;
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
    public class ClerkController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanClerkServices and injecting in ClerkController constructor
        /// </summary>
        private readonly ILoanClerkServices _clerkServices;
        public ClerkController(ILoanClerkServices loanClerkServices)
        {
            _clerkServices = loanClerkServices;
        }
        /// <summary>
        /// call the default get method to get all loan application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LoanMaster>> GetAllApplication()
        {
            return await _clerkServices.AllLoanApplication();
        }
        /// <summary>
        /// See the status of not recived application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("not-received")]
        public async Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Show/Get the status and list of recived loan application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("received")]
        public async Task<IEnumerable<LoanMaster>> RecivedLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Start the loan process after verify, //Make sure loan status is "recived" before process loan application
        /// Make the loan application as recived and check is it recived.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("process-loan/{loanId}")]
        public async Task<IActionResult> ProcessLoan([FromBody] LoanProcesstrans loanProcesstrans, int loanId)
        {
            //do code here
            throw new NotImplementedException();
        }
    }
}
