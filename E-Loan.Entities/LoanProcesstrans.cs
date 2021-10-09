using System;
using System.ComponentModel.DataAnnotations;

namespace E_Loan.Entities
{
    public class LoanProcesstrans
    {
        /// <summary>
        /// Use this table for loan process and for Clerk to add data
        /// </summary>
        [Key]
        public int Id { get; set; }
        [Display(Name = "Acres of Land")]
        [Required]
        public long AcresofLand { get; set; }
        [Display(Name = "Land Value In Rs")]
        [Required]
        public long LandValueinRs { get; set; }
        [Display(Name = "Appraised By")]
        [Required]
        public string AppraisedBy { get; set; }//Name of third party appraiser if any
        [Display(Name = "Valuation Date")]
        [Required]
        public DateTime ValuationDate { get; set; }
        [Display(Name = "Address of Property")]
        [Required]
        public string AddressofProperty { get; set; }
        [Display(Name = "Suggested Amount")]
        [Required]
        public long SuggestedAmount { get; set; }

        //Manager id ( Application to be forwarded to a particular manager : from the list of registered managers)
        [Required]
        public int ManagerId { get; set; }
        [Required]
        public int LoanId { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}