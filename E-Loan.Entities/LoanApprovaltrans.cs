using System;
using System.ComponentModel.DataAnnotations;

namespace E_Loan.Entities
{
    public class LoanApprovaltrans
    {    
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sanctioned Amount")]
        public double SanctionedAmount { get; set; }
        [Display(Name = "Term Plan in Year")]
        public double Termofloan { get; set; }
        [Display(Name = "Payment Start Date")]
        public DateTime PaymentStartDate { get; set; }
        [Display(Name = "Loan Closer Date")]
        public DateTime LoanCloserDate { get; set; }
        [Display(Name = "Monthly Payment")]
        public double MonthlyPayment { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
