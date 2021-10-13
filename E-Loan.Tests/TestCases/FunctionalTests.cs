using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.BusinessLayer.ViewModels;
using E_Loan.Controllers;
using E_Loan.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace E_Loan.Tests.TestCases
{
    public class FunctionalTests
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
        public FunctionalTests(ITestOutputHelper output)
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
        static FunctionalTests()
        {
            if (!File.Exists("../../../../output_revised.txt"))
                try
                {
                    File.Create("../../../../output_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_revised.txt");
                File.Create("../../../../output_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// This Test is use for test the applied loan application status by LoanId 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanStatusByLoanId()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.AppliedLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
                var result = await _customerServices.AppliedLoanStatus(_loanMaster.LoanId);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanStatusByLoanId=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanStatusByLoanId=" + res + "\n");
            return res;
        }

        /// <summary>
        /// Add/Apply a loan/Mortage using this function testing below method or test case.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ApplayMortage()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ApplayMortage=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ApplayMortage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this method try to  test mortage is updated or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateMortgage()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            var _updateLoan = new LoanMaster()
            {
                LoanId = 1,
                LoanName = "Personal-Loan",
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
            //Act
            try
            {
                customerservice.Setup(repo => repo.UpdateMortgage(_updateLoan.LoanId, _updateLoan)).ReturnsAsync(_updateLoan);
                var result = await _customerServices.UpdateMortgage(_updateLoan.LoanId, _updateLoan);
                if (result == _updateLoan)
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateMortgage=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateMortgage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this method or test get all loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetAllLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.AllLoanApplication());
                var result = await _clerkServices.AllLoanApplication();
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetAllLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetAllLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this test get all recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ReceivedLoanApplication());
                var result = await _clerkServices.ReceivedLoanApplication();
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetRecivedLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using the below test try to get recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetNotRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.NotReceivedLoanApplication());
                var result = await _clerkServices.NotReceivedLoanApplication();
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetNotRecivedLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetNotRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using the below method try to test loan application is processed or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ProcessLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ProcessLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ProcessLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using the below method try to test applied loan ststued is recived or not.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanRecivedLoan_ornot()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ReceivedLoan(_loanMaster.LoanId)).ReturnsAsync(_loanMaster);
                var result = await _clerkServices.ReceivedLoan(_loanMaster.LoanId);
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanRecivedLoan_ornot=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanRecivedLoan_ornot=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Try to test for manager to get all accepted loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetManagerAllLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AllLoanApplication());
                var result = await _managerServices.AllLoanApplication();
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetManagerAllLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetManagerAllLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using this test try to check and accept the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AcceptLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster); ;
                var result = await _managerServices.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AcceptLoanApplication_Manager=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AcceptLoanApplication_Manager=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using this test try to check and reject the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_RejectLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster);
                var result = await _managerServices.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_RejectLoanApplication_Manager=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_RejectLoanApplication_Manager=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using the below method try to test Sancationed loan is returining correct object or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_SanctionedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanMaster.LoanId, _loanApprovaltrans);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_SanctionedLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_SanctionedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Check loan ststus for manager before starting loan Sancation process
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_CheckLoanStatus_ForManager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.CheckLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster);
                var result = await _managerServices.CheckLoanStatus(_loanMaster.LoanId);
                //Assertion
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_CheckLoanStatus_ForManager=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_CheckLoanStatus_ForManager=" + res + "\n");
            return res;
        }
        /// <summary>
        /// try to test controller method return correct ActionResult type and json format or not.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> CustomerController_AppliedLoanStatus_Found_Or_Not()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.AppliedLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster);
                var controller = new CustomerController(_customerServices);
                var result = await controller.AppliedLoanStatus(_loanMaster.LoanId);

                var okResult = Assert.IsType<ActionResult<LoanMaster>>(result);
                var json = JsonConvert.SerializeObject(okResult.Value);
                var values = JsonConvert.DeserializeObject<LoanMaster>(json);

                if (values != null && values.LoanId == _loanMaster.LoanId)
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "CustomerController_AppliedLoanStatus_Found_Or_Not=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "CustomerController_AppliedLoanStatus_Found_Or_Not=" + res + "\n");
            return res;
        }
        /// <summary>
        /// try to test if user applay loan and object return as loanMaster test goes passed.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> CustomerController_ApplayMortage_ReturnObject()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            try
            {
                customerservice.Setup(repos => repos.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var controller = new CustomerController(_customerServices);
                var result = await controller.ApplayMortage(_loanMaster);
                var okResult = Assert.IsType<ActionResult<LoanMaster>>(result);
                var json = JsonConvert.SerializeObject(okResult.Value);
                var values = JsonConvert.DeserializeObject<LoanMaster>(json);
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "CustomerController_ApplayMortage_ReturnObject=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "CustomerController_ApplayMortage_ReturnObject=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as IEnumerable<LoanMaster> make true and test pass.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ClerkController_GetAllApplication_Found_Or_Not()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.AllLoanApplication());
                ClerkController controller = new ClerkController(_clerkServices);
                var result = await controller.GetAllApplication() as IEnumerable<LoanMaster>;
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetAllApplication_Found_Or_Not=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetAllApplication_Found_Or_Not=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as IEnumerable<LoanMaster> that loan status is not recived, make true and test pass.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ClerkController_GetNotRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.NotReceivedLoanApplication());
                ClerkController controller = new ClerkController(_clerkServices);
                var result = await controller.NotRecivedLoanApplication() as IEnumerable<LoanMaster>;
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetNotRecivedLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetNotRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as IEnumerable<LoanMaster> that loan status is recived, make true and test pass.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ClerkController_GetRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ReceivedLoanApplication());
                ClerkController controller = new ClerkController(_clerkServices);
                var result = await controller.RecivedLoanApplication() as IEnumerable<LoanMaster>;
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetRecivedLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ClerkController_GetRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as IEnumerable<LoanMaster> make true and test pass for manager
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ManagerController_GetAllApplication_Found_Or_Not()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AllLoanApplication());
                var controller = new ManagerController(_managerServices);
                var result = await controller.GetAllApplication() as IEnumerable<LoanMaster>;
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_GetAllApplication_Found_Or_Not=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_GetAllApplication_Found_Or_Not=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as true or false while accepting application make true and test pass for manager
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ManagerController_AcceptLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AcceptLoanApplication(_loanMaster.LoanId,"Test")).ReturnsAsync(_loanMaster);
                var controller = new ManagerController(_managerServices);
                var result = await controller.AcceptLoanApplication(_loanMaster.LoanId, "Test");
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_AcceptLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_AcceptLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// check return result as true or false while Rejecting application make true and test pass for manager
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> ManagerController_RejectLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.RejectLoanApplication(_loanMaster.LoanId, "Test")).ReturnsAsync(_loanMaster);
                var controller = new ManagerController(_managerServices);
                var result = await controller.RejectLoanApplication(_loanMaster.LoanId, "Test");
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
                await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_RejectLoanApplication=" + res + "\n");
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
            await File.AppendAllTextAsync("../../../../output_revised.txt", "ManagerController_RejectLoanApplication=" + res + "\n");
            return res;
        }
    }
}