using System;using System.ComponentModel.DataAnnotations;

namespace E_Loan.Entities
{
    public class LoanMaster
    {
        public LoanMaster()
        {
            LStatus = LoanStatus.NotReceived;
            BusinessStructure = BusinessStatus.Individual;
        }
        [Key]
        public int LoanId { get; set; }
        [Required]
        [MinLength(5)]
        public string LoanName { get; set; }
        public long LoanAmount { get; set; }
        [Display(Name = "Loan Apply Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public BusinessStatus? BusinessStructure { get; set; }
        [Required]
        public BillingStatus? Billing_Indicator { get; set; }
        [Required]
        public TaxStatus? Tax_Indicator { get; set; }
        public string ContactAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AppliedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string ManagerRemark { get; set; }
        [Required]
        [Display(Name = "Loan Status")]
        public LoanStatus? LStatus { get; set; }
    }
}
