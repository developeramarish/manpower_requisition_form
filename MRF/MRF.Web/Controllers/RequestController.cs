using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Utility;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail.Model;
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
                mrfDetailsStatusHistory mrfDetailsStatusHistory = _unitOfWork.mrfDetailsStatusHistory.Get(u => u.MrfId == mrfID && u.mrfStatusId == mrfStatusId);
                if(mrfDetailsStatusHistory != null)
                {
                    ViewData["AlreadyApproved"] = "AlreadyApproved";
                    return View();
                }
                mrfDetailsStatusHistory mrfAlreadyProcessed = _unitOfWork.mrfDetailsStatusHistory.Get(u => u.MrfId == mrfID && u.CreatedByEmployeeId == updatedByEmployeeId);
                if (mrfAlreadyProcessed != null)
                {
                    ViewData["AlreadyProcessed"] = "AlreadyProcessed";
                    return View();
                }

                HttpResponseMessage response = await ChangeMrfStatusAsync(mrfID, mrfStatusId, updatedByEmployeeId);
                _logger.LogInfo("response code = " + response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
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
        private async Task<HttpResponseMessage> ChangeMrfStatusAsync(int mrfID, int mrfStatusId, int updatedByEmployeeId)
        {
            try
            {               
                using (var httpClientHandler = new HttpClientHandler())
                { 
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        var accessToken = GetAccessToken();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);                        
                        var apiUrl = _configuration["AppUrl"] + mrfID;
                        var requestBody = new
                        {
                            mrfID = mrfID,
                            mrfStatusId = mrfStatusId,
                            updatedByEmployeeId = updatedByEmployeeId,
                            updatedOnUtc = DateTime.UtcNow
                        };

                        string jsonPayloadString = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                        StringContent content = new StringContent(jsonPayloadString, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);
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
                mrfDetailsStatusHistory mrfDetailsStatusHistory = _unitOfWork.mrfDetailsStatusHistory.Get(u => u.MrfId == mrfID && u.mrfStatusId == mrfStatusId);
                if (mrfDetailsStatusHistory != null)
                {
                    ViewData["AlreadyRejected"] = "AlreadyRejected";
                    return View();
                }
                mrfDetailsStatusHistory mrfAlreadyProcessed = _unitOfWork.mrfDetailsStatusHistory.Get(u => u.MrfId == mrfID && u.CreatedByEmployeeId == updatedByEmployeeId);
                if (mrfAlreadyProcessed != null)
                {
                    ViewData["AlreadyProcessed"] = "AlreadyProcessed";
                    return View();
                }
                HttpResponseMessage response = await ChangeMrfStatusAsync(mrfID, mrfStatusId, updatedByEmployeeId);
                if (response.IsSuccessStatusCode)
                {
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
    }
}
