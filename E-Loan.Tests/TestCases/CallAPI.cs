using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.Tests.TestCases
{
    public class CallAPI
    {
        static string token = "";
        static string email = "";
        static string tokenUrl = "https://staging.yaksha.online/yaksha-multifile/api/v1/token";
        static string saveResultUrl = "https://staging.yaksha.online/yaksha-multifile/api/v1/saveTestCaseInstance";
        /// <summary>
        /// Save test result on API
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        public static async Task<string> saveTestResult(string testName, string status, string type)
        {
            if (email.Equals(""))
            {
                email = Decode64Email();
            }
            if (token.Equals(""))
            {
                GetToken tokenList = new GetToken();
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(tokenList), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync(tokenUrl, content))
                        {
                            try
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                tokenList = JsonConvert.DeserializeObject<GetToken>(apiResponse);
                                token = tokenList.token;
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }

                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            using (var httpClient = new HttpClient())
            {
                TestData testData = new TestData();
                testData.companyId = "IH";
                testData.companyName = "IIHT";
                testData.output = status;
                testData.questionText = "optional";
                testData.testCaseName = testName;
                testData.testCaseType = type;
                testData.testName = "Dummy Q Dotnet local storage";
                testData.user = email;
                testData.version = 0;

                StringContent content = new StringContent(JsonConvert.SerializeObject(testData), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("token", token);
                using (var response = await httpClient.PostAsync(saveResultUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return status;
        }
        /// <summary>
        /// Get user email
        /// </summary>
        /// <returns></returns>
        public static string Decode64Email()
        {
            string decodestring = null;
            try
            {
                decodestring = System.IO.File.ReadAllText("../../../../1000.props");
                var base64EncodedBytes = System.Convert.FromBase64String(decodestring);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
                throw (e);
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                throw (e);
            }
        }
        /// <summary>
        /// Get test Method Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCurrentMethodName([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            return name;
        }
    }
}
