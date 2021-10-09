using E_Loan.Entities;
using System.ComponentModel.DataAnnotations;

namespace E_Loan.BusinessLayer
{
    public class UserMasterRegisterModel
    {
        public UserMasterRegisterModel()
        {
            Enabled = false;
        }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public IdProofType? IdproofTypes { get; set; }
        public string IdProofNumber { get; set; }
        public bool Enabled { get; set; }
    }
}
