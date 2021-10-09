using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.Entities
{
    public enum IdProofType
    {
        Aadhar = 1,
        Passport,
        PAN,
        [Display(Name= "Driving Licence")]
        Driving_Licence,
        [Display(Name = "Voter Card")]
        Voter_Card,
        [Display(Name = "Ration Card")]
        Ration_card
    }
}
