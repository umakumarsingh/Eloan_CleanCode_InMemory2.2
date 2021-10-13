using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.BusinessLayer.ViewModels;
using E_Loan.Entities;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace E_Loan.Tests.TestCases
{
    public class ExceptionalTest
    {
        /// <summary>
        /// Creating Referance Variable and Mocking repository class
        /// </summary>
        private readonly ITestOutputHelper _output;
        private readonly ILoanCustomerServices _customerServices;
        private readonly ILoanClerkServices _clerkServices;
        private readonly ILoanManagerServices _managerServices;
        public readonly Mock<ILoanCustomerRepository> customerservice = new Mock<ILoanCustomerRepository>();
        public readonly Mock<ILoanClerkRepository> clerkservice = new Mock<ILoanClerkRepository>();
        public readonly Mock<ILoanManagerRepository> managerservice = new Mock<ILoanManagerRepository>();

        private LoanMaster _loanMaster;
        private UserMaster _userMaster;
        private LoanProcesstrans _loanProcesstrans;
        private LoanApprovaltrans _loanApprovaltrans;
        private readonly LoanApprovalViewModel _loanApproval;
        public ExceptionalTest(ITestOutputHelper output)
        {
            /// <summary>
            /// Injecting service object into Test class constructor
            /// </summary>
            _customerServices = new LoanCustomerServices(customerservice.Object);
            _clerkServices = new LoanClerkServices(clerkservice.Object);
            _managerServices = new LoanManagerServices(managerservice.Object);
            _output = output;
            _loanMaster = new LoanMaster
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
            };
            _userMaster = new UserMaster
            {
                Id = 1,
                Name = "Kundan",
                Email = "umakumarsingh@techademy.com",
                Contact = "9631438123",
                Address = "Gaya",
                IdproofTypes = IdProofType.Aadhar,
                IdProofNumber = "AYIPK6551B",
                Enabled = false
            };
            _loanProcesstrans = new LoanProcesstrans
            {
                Id = 1,
                AcresofLand = 1,
                LandValueinRs = 4700000,
                AppraisedBy = "Uma",
                ValuationDate = DateTime.Now,
                AddressofProperty = "Mall - Karnataka",
                SuggestedAmount = 4000000,
                ManagerId = 2,
                LoanId = 1
            };
            _loanApprovaltrans = new LoanApprovaltrans
            {
                Id = 1,
                SanctionedAmount = 4000000,
                Termofloan = 72,
                PaymentStartDate = DateTime.Now,
                LoanCloserDate = DateTime.Now,
                MonthlyPayment = 3330000
            };
            _loanApproval = new LoanApprovalViewModel
            {
                SanctionedAmount = 123456,
                Termofloan = 12,
                PaymentStartDate = DateTime.Now
            };
        }
        /// <summary>
        /// Creating test output text file that store the result in boolean result
        /// </summary>
        static ExceptionalTest()
        {
            if (!File.Exists("../../../../output_exception_revised.txt"))
                try
                {
                    File.Create("../../../../output_exception_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_exception_revised.txt");
                File.Create("../../../../output_exception_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// Test to validate if user pass the null object while apply mortage, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidApplyMortage()
        {
            //Arrange
            bool res = false;
            _loanMaster = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster = null);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result == null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidApplyMortage=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidApplyMortage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if clerk pass the null object while Process Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidProcessLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanProcesstrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans = null);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result == null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidProcessLoanTrans=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidProcessLoanTrans=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if clerk pass the null object while Sanctioned Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidSanctionedLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanApprovaltrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans = null);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                if (result == null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidSanctionedLoanTrans=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidSanctionedLoanTrans=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if user pass the null loan id or 0 while finding mortage, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_ifInvalidLoanIdIsPassedandLoanNotFound()
        {
            //Arrange
            bool res = false;
            var loanId = 1;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.AppliedLoanStatus(loanId)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.AppliedLoanStatus(loanId);
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_ifInvalidLoanIdIsPassedandLoanNotFound=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_ifInvalidLoanIdIsPassedandLoanNotFound=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan name must be greater then 5 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidLoanNamePassed_LoanNameMinFiveCharactor()
        {
            //Arrange
            bool res = false;
            _loanProcesstrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result != null && result.LoanName.Length > 5)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidLoanNamePassed_LoanNameMinFiveCharactor=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidLoanNamePassed_LoanNameMinFiveCharactor=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan process id must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcessIdIsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcessIdIsvalidorNot_Return=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcessIdIsvalidorNot_Return=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan process acres of land must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_AcresOfLand_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.AcresofLand > 0)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_AcresOfLand_IsvalidorNot_Return=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_AcresOfLand_IsvalidorNot_Return=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan process Land ValueinRs must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_LandValueinRs_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.LandValueinRs > 0)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_LandValueinRs_IsvalidorNot_Return=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_LandValueinRs_IsvalidorNot_Return=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan process Suggested Amount must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_SuggestedAmount_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.SuggestedAmount > 0)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_SuggestedAmount_IsvalidorNot_Return=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_SuggestedAmount_IsvalidorNot_Return=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if loan process manager Id must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_ManagerId_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_ManagerId_IsvalidorNot_Return=" + res + "\n");
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_VaidateLoanProcess_ManagerId_IsvalidorNot_Return=" + res + "\n");
            return res;
        }
    }
}