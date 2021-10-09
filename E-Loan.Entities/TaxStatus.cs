using System.ComponentModel.DataAnnotations;

namespace E_Loan.Entities
{
    public enum TaxStatus
    {
        
        [Display(Name = "Tax Payer")]
        Tax_Payer,
        [Display(Name = "Not Tax Payer")]
        Not_tax_Payer
    }
}
