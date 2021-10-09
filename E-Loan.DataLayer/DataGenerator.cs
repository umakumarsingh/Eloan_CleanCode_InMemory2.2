using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Loan.DataLayer
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ELoanDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ELoanDbContext>>()))
            {
                if (context.userMasters.Any())
                {
                    return;   // Data was already seeded
                }
                context.userMasters.AddRange(
                    new UserMaster
                    {
                        Id = 1,
                        Name = "Kundan",
                        Email = "umakumarsingh@techademy.com",
                        Contact = "9631438123",
                        Address = "Gaya",
                        IdproofTypes = IdProofType.Aadhar,
                        IdProofNumber = "AYIPK6551B",
                        Enabled = false
                    });
                context.SaveChanges();
                if (context.loanMasters.Any())
                {
                    return;   // Data was already seeded
                }
                context.loanMasters.AddRange(
                    new LoanMaster
                    {
                        LoanId = 1,
                        LoanName = "Home Loan",
                        Date = System.DateTime.Now,
                        BusinessStructure = BusinessStatus.Individual,
                        Billing_Indicator = BillingStatus.Not_Salaried_Person,
                        Tax_Indicator = TaxStatus.Not_tax_Payer,
                        ContactAddress = "Gaya-Bihar",
                        Phone = "9632584754",
                        Email = "eloan@iiht.com",
                        AppliedBy = "Kumar",
                        CreatedOn = DateTime.Now,
                        ManagerRemark = "Ok",
                        LStatus = LoanStatus.NotReceived
                    });
                context.SaveChanges();
                if (context.loanProcesstrans.Any())
                {
                    return;   // Data was already seeded
                }
                context.loanProcesstrans.AddRange(
                    new LoanProcesstrans
                    {
                        Id = 1,
                        AcresofLand = 1,
                        LandValueinRs = 4700000,
                        AppraisedBy = "Uma",
                        ValuationDate = DateTime.Now,
                        AddressofProperty = "Mall - Karnataka",
                        SuggestedAmount = 4000000,
                        ManagerId = 1,
                        LoanId = 1
                    });
                context.SaveChanges();
                if (context.loanApprovaltrans.Any())
                {
                    return;   // Data was already seeded
                }
                context.loanApprovaltrans.AddRange(
                    new LoanApprovaltrans
                    {
                        Id = 1,
                        SanctionedAmount = 4000000,
                        Termofloan = 72,
                        PaymentStartDate = DateTime.Now,
                        LoanCloserDate = DateTime.Now,
                        MonthlyPayment = 3330000
                    });
                context.SaveChanges();
            }
        }
    }
}
