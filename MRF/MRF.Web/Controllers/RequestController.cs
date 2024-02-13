using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Utility;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;


namespace MRF.Web.Controllers
{
    public class RequestController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly SmtpClient smtpClient;
        private string mrfUrl = string.Empty;
        private readonly string senderEmail;
        public RequestController(IConfiguration configuration,
            ILoggerService logger,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;

            senderEmail = _configuration["SMTP:senderEmail"];
            smtpServer = _configuration["SMTP:Server"];
            smtpPort = Convert.ToInt32(_configuration["SMTP:Port"]);
            smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false;
        }
        public async Task<IActionResult> Approve([FromQuery(Name = "MrfId")] int mrfID, [FromQuery(Name = "StatusId")] int mrfStatusId, [FromQuery(Name = "EmpId")] int updatedByEmployeeId)
        {
            try
            {
                _logger.LogInfo("Entered into Approve method");

                HttpResponseMessage response = await ChangeMrfStatusAsync(mrfID, mrfStatusId, updatedByEmployeeId);
                _logger.LogInfo("response code = " + response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInfo("Request Controller mrfID = " + mrfID);
                    _logger.LogInfo("Request Controller mrfStatusId = " + mrfStatusId);
                    _logger.LogInfo("Entered into Approve method");
                    await SendEmailAsync(mrfID, mrfStatusId);
                    ViewData["Message"] = "MRF has been approved successfully!";
                    return View();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private async Task SendEmailAsync(int mrfID, int mrfStatusId)
        {
            try
            {
                _logger.LogDebug($"SendEmailAsync MRF ID: {mrfID}");
                _logger.LogDebug($"SendEmailAsync mrfStatusId: {mrfStatusId}");
                Mrfdetails mrfdetails = _unitOfWork.Mrfdetail.Get(u => u.Id == mrfID);

                Mrfstatusmaster mrfstatusmaster = _unitOfWork.Mrfstatusmaster.Get(u => u.Id == mrfStatusId);
                _logger.LogDebug($"SendEmailAsync mrfstatusmaster.Status: {mrfstatusmaster.Status}");
                emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == mrfstatusmaster.Status);
                _logger.LogDebug($"SendEmailAsync emailRequest.roleId: {emailRequest.roleId}");
                string[] roleIdStrings = emailRequest.roleId.Split(',');
                List<int> roleIds = new List<int>();

                foreach (string roleIdString in roleIdStrings)
                {
                    if (int.TryParse(roleIdString, out int roleId))
                    {
                        roleIds.Add(roleId);
                    }
                }

                mrfUrl = _configuration["MRFUrl"].Replace("ID", mrfID.ToString());
                _logger.LogDebug($"SendEmailAsync mrfUrl: {mrfUrl}");
                List<string> email = (from employeeDetails in _unitOfWork.Employeedetails.GetAll()
                                      where (from employeeRoleMap in _unitOfWork.Employeerolemap.GetAll()
                                             where (from mrfEmailApproval in _unitOfWork.MrfEmailApproval.GetAll()
                                                    where mrfEmailApproval.MrfId == mrfID
                                                    select mrfEmailApproval.EmployeeId).Contains(employeeRoleMap.EmployeeId) &&
                                                   roleIds.Contains(employeeRoleMap.RoleId)
                                             select employeeRoleMap.EmployeeId).Contains(employeeDetails.Id)
                                      select employeeDetails.Email).ToList();

                foreach (string strEmail in email)
                {
                    _logger.LogDebug($"SendEmailAsync email: {strEmail}");
                    string htmlContent = emailRequest.Content.Replace("MRF ##", $"<span style='color:red; font-weight:bold;'>MRF Id {mrfdetails.ReferenceNo}</span>")
                                                         .Replace("click here", $"<span style='color:blue; font-weight:bold; text-decoration:underline;'><a href='{mrfUrl}'>click here</a></span>");


                    _logger.LogDebug($"SendEmailAsync strEmail: {strEmail}");
                    _logger.LogDebug($"SendEmailAsync emailRequest.Subject: {emailRequest.Subject}");
                    _logger.LogDebug($"SendEmailAsync htmlContent: {htmlContent}");


                    using (MailMessage mailMessage = new MailMessage(senderEmail, strEmail, emailRequest.Subject, htmlContent))
                    {
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while sending email: {e.Message}");
                throw;
            }
        }

        private async Task<HttpResponseMessage> ChangeMrfStatusAsync(int mrfID, int mrfStatusId, int updatedByEmployeeId)
        {
            try
            {
                _logger.LogInfo("mrfID = " + mrfID);
                _logger.LogInfo("mrfStatusId = " + mrfStatusId);
                _logger.LogInfo("updatedByEmployeeId = " + updatedByEmployeeId);
                using (var httpClientHandler = new HttpClientHandler())
                {
                    _logger.LogInfo("ServerCertificateCustomValidationCallback - Start");
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                    _logger.LogInfo("ServerCertificateCustomValidationCallback - End");
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        _logger.LogInfo("GetAccessToken - Start");
                        var accessToken = GetAccessToken();
                        _logger.LogInfo("GetAccessToken - End");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);                        
                        var apiUrl = _configuration["AppUrl"] + mrfID;
                        _logger.LogInfo("apiUrl = " + apiUrl);
                        var requestBody = new
                        {
                            mrfID = mrfID,
                            mrfStatusId = mrfStatusId,
                            updatedByEmployeeId = updatedByEmployeeId,
                            updatedOnUtc = DateTime.UtcNow
                        };

                        string jsonPayloadString = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                        _logger.LogInfo("jsonPayloadString = " + jsonPayloadString);
                        StringContent content = new StringContent(jsonPayloadString, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                        _logger.LogInfo("response = " + response);
                        return response;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetAccessToken()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/742bc209-0ce8-4cf8-b2e2-32d4d1c2d9ea/oauth2/v2.0/token");
            request.Headers.Add("Cookie", "fpc=ApF-DnF1OMRDobPwaJJtK2hMlKTdAQAAAMKUIN0OAAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("client_id", "a0eabf6a-94bb-4a18-82c9-bf5e5486f945"));
            collection.Add(new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"));
            collection.Add(new KeyValuePair<string, string>("client_secret", "7uQ8Q~FVVkGH3UmlDVAKiQqWnFpqSb3-6R-wJbin"));
            collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            var response = client.SendAsync(request).Result; // Synchronously waiting for the response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result; // Synchronously waiting for the content
                                                                                   // Assuming responseContent contains JSON with an access_token field
                var responseData = JObject.Parse(responseContent);
                return responseData["access_token"].ToString();
            }
            else
            {
                // Handle authentication failure
                throw new Exception("Failed to retrieve access token");
            }
        }
        public async Task<IActionResult> Reject([FromQuery(Name = "MrfId")] int mrfID, [FromQuery(Name = "StatusId")] int mrfStatusId, [FromQuery(Name = "EmpId")] int updatedByEmployeeId)
        {
            try
            {
                HttpResponseMessage response = await ChangeMrfStatusAsync(mrfID, mrfStatusId, updatedByEmployeeId);
                if (response.IsSuccessStatusCode)
                {
                    await SendEmailAsync(mrfID, mrfStatusId);
                    ViewData["Message"] = "MRF has been rejected successfully!";
                    return View();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Bypass([FromQuery(Name = "MrfId")] int mrfID, [FromQuery(Name = "StatusId")] int mrfStatusId, [FromQuery(Name = "EmpId")] int updatedByEmployeeId)
        {
            try
            {
                HttpResponseMessage response = await ChangeMrfStatusAsync(mrfID, mrfStatusId, updatedByEmployeeId);
                if (response.IsSuccessStatusCode)
                {
                   // _emailService.SendEmail("manish.partey@kwglobal.com", "Test", "Test");
                    return Ok("MRF has been bypassed successfully!");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
