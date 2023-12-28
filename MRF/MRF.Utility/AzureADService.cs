using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace MRF.Utility
{
    public class AzureADService
    {
        private static readonly string tenantId = "742bc209-0ce8-4cf8-b2e2-32d4d1c2d9ea";
        private static readonly string clientId = "a0eabf6a-94bb-4a18-82c9-bf5e5486f945";
        private static readonly string clientSecret = "7uQ8Q~FVVkGH3UmlDVAKiQqWnFpqSb3-6R-wJbin";
        private static readonly string graphResource = "https://graph.microsoft.com";
        private static readonly string graphApiVersion = "v1.0";

        public async Task<string> GetAccessToken()
        {
            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                    .Build();

            string[] scopes = new string[] { $"{graphResource}/.default" };

            var result = await app.AcquireTokenForClient(scopes)
                                  .ExecuteAsync();

            return result.AccessToken;
        }

        public async Task<string> GetUsers(string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync($"{graphResource}/{graphApiVersion}/users");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<string> GetGroups(string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync($"{graphResource}/{graphApiVersion}/groups");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
