using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SianApi.Librerias.Ubereats
{
    public class UberClient
    {
        private static string baseUri = "https://api.uber.com/v1/eats/stores/";
        private static string tokenUri = "https://login.uber.com/oauth/v2/token/";
        private static string clientId = "R-JiKwsX5CQD6kAsXGTLv7dQPAzRU1j1";
        private static string clientSecret = "DLhvsDDoVx0KmhnZNOwzLj0qcBlLwUJSvxHyN-Kq";
        private static string clientIdPrd = "K3wW61bDG9z8hPsaDNRjUzzRbnk7y7vu";
        private static string clientSecretPrd = "phCQ5ujNwQeT_1gz-3DhR1NYyYehy8TqeQIypWUy";
        private static string storeIdPopeyes = "d84c0fc3-cf8a-4130-a0bb-d26e073a02b4"; // Popeyes Store ID Uber
        private static string storeIdPapajohns = "791c41ae-c7b5-43a2-bd7b-d652f12f56ce"; // PapaJohns Store ID Uber
        private static string storeIdBembos = "b7955551-01d0-4a98-9dc1-4fff2473f15b"; // Bembos Store ID Uber
        private static string storeIdChinawok = "0e5e84c2-8987-4029-8d1a-e02a68935442"; // Chinawok Store ID Uber
        private static string storeIdDonbelisario = "4c40af74-1e86-4472-b9e7-fda5a21ae7fe"; // Don Belisario Store ID Uber
        private static string token;

        public async Task<string> GetTokenAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Dictionary<string, string> tokenDetails = null;
                httpClient.BaseAddress = new Uri(tokenUri);
                var login = new Dictionary<string, string>
                   {
                       {"client_id", clientId},
                       {"client_secret", clientSecret},
                       {"grant_type", "client_credentials"},
                       {"scope", "eats.store"}
                   };
                var response = await httpClient.PostAsync(httpClient.BaseAddress, new FormUrlEncodedContent(login));
                if (response.IsSuccessStatusCode)
                {
                    tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    if (tokenDetails != null && tokenDetails.Any())
                    {
                        var accessToken = tokenDetails.FirstOrDefault().Value;
                        token = accessToken.ToString();
                    }
                }
                return token;
            }
        }

        public async Task<string> GetTokenAsyncPrd()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Dictionary<string, string> tokenDetails = null;
                httpClient.BaseAddress = new Uri(tokenUri);
                var login = new Dictionary<string, string>
                {
                    {"client_id", clientIdPrd},
                    {"client_secret", clientSecretPrd},
                    {"grant_type", "client_credentials"},
                    {"scope", "eats.store"}
                };
                var response = await httpClient.PostAsync(httpClient.BaseAddress, new FormUrlEncodedContent(login));
                if (response.IsSuccessStatusCode)
                {
                    tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    if (tokenDetails != null && tokenDetails.Any())
                    {
                        var accessToken = tokenDetails.FirstOrDefault().Value;
                        token = accessToken.ToString();
                    }
                }
                return token;
            }
        }

        public async Task<HttpResponseMessage> PutMenuAsync(string token, string json, string marca, string tiendaPrdUberId = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                if (tiendaPrdUberId == null)
                {
                    switch (marca)
                    {
                        case "Popeyes":
                            httpClient.BaseAddress = new Uri(@baseUri + storeIdPopeyes + "/menu");
                            break;
                        case "Papa Johns":
                            httpClient.BaseAddress = new Uri(@baseUri + storeIdPapajohns + "/menu");
                            break;
                        case "Bembos":
                            httpClient.BaseAddress = new Uri(@baseUri + storeIdBembos + "/menu");
                            break;
                        case "China Wok":
                            httpClient.BaseAddress = new Uri(@baseUri + storeIdChinawok + "/menu");
                            break;
                        case "Don Belisario":
                            httpClient.BaseAddress = new Uri(@baseUri + storeIdDonbelisario + "/menu");
                            break;
                    }
                }
                else
                {
                    httpClient.BaseAddress = new Uri(@baseUri + tiendaPrdUberId + "/menu");
                }

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = await httpClient.PutAsync(httpClient.BaseAddress.ToString(), new StringContent(json, Encoding.UTF8, "application/json"));
                return response;
            }
        }
    }
}