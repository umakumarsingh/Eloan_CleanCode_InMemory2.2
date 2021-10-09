using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.BusinessLayer.ViewModels
{
    public class LoanApprovalViewModel
    {
        public LoanApprovalViewModel()
        {
            double intrestRate = .06;
            double monthlyPay = PMT(intrestRate, (int)Termofloan, SanctionedAmount);
            MonthlyPayment = monthlyPay;

            //Calculate Loan Closer Date
            var cloaserDate = PaymentStartDate.AddMonths((int)Termofloan);
            LoanCloserDate = cloaserDate;
        }
        public static double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
        {
            var rate = (double)yearlyInterestRate / 100 / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return (rate + (rate / denominator)) * loanAmount;
        }
        [Required]
        [Display(Name = "Sanctioned Amount")]
        public double SanctionedAmount { get; set; }
        [Required]
        [Display(Name = "Term Plan in Year")]
        public double Termofloan { get; set; }
        [Required]
        [Display(Name = "Payment Start Date")]
        public DateTime PaymentStartDate { get; set; }
        public DateTime LoanCloserDate { get; set; }
        [Display(Name = "Monthly Payment")]
        public double MonthlyPayment { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
