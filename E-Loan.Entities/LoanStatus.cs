using System.ComponentModel.DataAnnotations;

namespace E_Loan.Entities
{
    /// <summary>
    /// This Enum define key value pair for Loan Status. its bind on Customer loan Status
    /// </summary>
    public enum LoanStatus
    {
        [Display(Name = "Not Recived")]
        NotReceived = 1,
        Received = 2,
        Accept = 3,
        Rejected =4,
        Done = 5
    }
}
