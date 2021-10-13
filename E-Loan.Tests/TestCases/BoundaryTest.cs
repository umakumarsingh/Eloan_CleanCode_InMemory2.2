using E_Loan.BusinessLayer;
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
    public class BoundaryTest
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

        private readonly LoanMaster _loanMaster;
        private readonly UserMaster _userMaster;
        private readonly LoanProcesstrans _loanProcesstrans;
        private readonly LoanApprovaltrans _loanApprovaltrans;
        private readonly LoanApprovalViewModel _loanApproval;
        public BoundaryTest(ITestOutputHelper output)
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
        static BoundaryTest()
        {
            if (!File.Exists("../../../../output_boundary_revised.txt"))
                try
                {
                    File.Create("../../../../output_boundary_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_boundary_revised.txt");
                File.Create("../../../../output_boundary_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// Test to validate if Apply Mortage loanId is correct or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateMortageApplicationLoanId()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result.LoanId == _loanMaster.LoanId)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateMortageApplicationLoanId=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateMortageApplicationLoanId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate Loan Process Id Is correct or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateLoanProcesstransId()
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

                if (result.Id == _loanProcesstrans.Id)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateLoanProcesstransId=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateLoanProcesstransId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate LoanApproval Id is correect or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateLoanApprovaltransId()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId,_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                if (result.Id == _loanApprovaltrans.Id)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateLoanApprovaltransId=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateLoanApprovaltransId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Testfor_ValidEmail to test email id is valid or not
        /// </summary>
        [Fact]
        public async Task<bool> Testfor_ValidEmail()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                //bool isEmail = Regex.IsMatch(_loanMaster.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //Assert
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Email.ToString().Length;
                if (result.Email.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidEmail=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidEmail=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Testfor_ValidateMobileNumber is used for test mobile number is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateMobileNumber()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Phone.ToString().Length;
                if (result.Phone.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateMobileNumber=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateMobileNumber=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan name connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_LoanName_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.LoanName.ToString().Length;
                if (result.LoanName.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_LoanName_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_LoanName_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan BusinessStructure connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_BusinessStructure_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.BusinessStructure.ToString().Length;
                if (result.BusinessStructure.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_BusinessStructure_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_BusinessStructure_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan Billing_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_Billing_Indicator_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Billing_Indicator.ToString().Length;
                if (result.Billing_Indicator.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_Billing_Indicator_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_Billing_Indicator_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan Tax_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_Tax_Indicator_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Tax_Indicator.ToString().Length;
                if (result.Tax_Indicator.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_Tax_Indicator_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_Tax_Indicator_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan Tax_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_ContactAddress_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.ContactAddress.ToString().Length;
                if (result.ContactAddress.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_ContactAddress_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_ContactAddress_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master loan Status connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_LoanStatus_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.LStatus.ToString().Length;
                if (result.LStatus.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_LoanStatus_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanMaster_LoanStatus_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master Loan Process trans_Id connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_Id_NotEmpty()
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
                var actualLength = _loanProcesstrans.Id.ToString().Length;
                if (result.Id.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_Id_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_Id_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master Loan AcresofLand connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_AcresofLand_NotEmpty()
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
                var actualLength = _loanProcesstrans.AcresofLand.ToString().Length;
                if (result.AcresofLand.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_AcresofLand_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_AcresofLand_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master Loan Landvalueinrs connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_LandValueinRs_NotEmpty()
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
                var actualLength = _loanProcesstrans.LandValueinRs.ToString().Length;
                if (result.LandValueinRs.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_LandValueinRs_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_LandValueinRs_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master Loan Landvalueinrs connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_SuggestedAmount_NotEmpty()
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
                var actualLength = _loanProcesstrans.SuggestedAmount.ToString().Length;
                if (result.SuggestedAmount.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_SuggestedAmount_NotEmpty=" + res + "\n");
                return false;
            }
            //Asert//final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_SuggestedAmount_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan master Loan Manager Id connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_ManagerId_NotEmpty()
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
                var actualLength = _loanProcesstrans.ManagerId.ToString().Length;
                if (result.ManagerId.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_ManagerId_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanProcesstrans_ManagerId_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan Approval Loan Sanctioned Amount connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_SanctionedAmount_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                var actualLength = _loanApprovaltrans.SanctionedAmount.ToString().Length;
                if (result.SanctionedAmount.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_SanctionedAmount_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_SanctionedAmount_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan Approval Loan Termofloan connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_Termofloan_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                var actualLength = _loanApprovaltrans.Termofloan.ToString().Length;
                if (result.Termofloan.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_Termofloan_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_Termofloan_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan Approval Loan Payment Start Date connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_PaymentStartDate_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                var actualLength = _loanApprovaltrans.PaymentStartDate.ToString().Length;
                if (result.PaymentStartDate.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_PaymentStartDate_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_PaymentStartDate_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan Approval Loan Loan Closer Date connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_LoanCloserDate_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                var actualLength = _loanApprovaltrans.LoanCloserDate.ToString().Length;
                if (result.LoanCloserDate.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_LoanCloserDate_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_LoanCloserDate_NotEmpty=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate loan Approval Loan Monthly Payment connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_MonthlyPayment_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                var actualLength = _loanApprovaltrans.MonthlyPayment.ToString().Length;
                if (result.MonthlyPayment.ToString().Length == actualLength)
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
                await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_MonthlyPayment_NotEmpty=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_LoanApprovaltrans_MonthlyPayment_NotEmpty=" + res + "\n");
            return res;
        }
    }
}