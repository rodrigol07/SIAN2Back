using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SianApi.Librerias.Glovo
{
    public class GlovoClient
    {
        private static string baseUri = "https://stageapi.glovoapp.com/webhook/stores/";
        //private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkdMT1ZPIiwibmJmIjoxNTM4MDAyNTg0LCJleHAiOjE1MzgwMDI4ODQsImlhdCI6MTUzODAwMjU4NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NjYwNCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTY2MDQifQ.7xYdV44obl0ml-dLcvcLWYX3uOIiRYJfu-2nB02GdSs";
        private static string data;
        private static string token;
        private static string storeId;
        private static string bbStoreId = "7777";
        private static string ddStoreId = "3333";
        private static string pjStoreId = "4444";
        private static string cwStoreId = "2222";
        private static string ppStoreId = "1111";
        private static string dbStoreId = "6666";
        private static string whStoreId = "5555";

        public async Task<HttpResponseMessage> PostMenuAsync(string marca, string jsonFileName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkdMT1ZPIiwibmJmIjoxNTM4MDAyNTg0LCJleHAiOjE1MzgwMDI4ODQsImlhdCI6MTUzODAwMjU4NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NjYwNCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTY2MDQifQ.7xYdV44obl0ml-dLcvcLWYX3uOIiRYJfu-2nB02GdSs";

                switch (marca)
                {
                    case "Popeyes":
                        storeId = ppStoreId;
                        token = "e967e465-9c0a-4500-87da-595e1b0be248";
                        break;

                    case "China Wok":
                        storeId = cwStoreId;
                        token = "9f0f981a-b3fd-4279-8ea5-4785ca87eabd";
                        break;

                    case "dunkindonuts":
                        storeId = ddStoreId;
                        token = "8e29087b-de63-4866-b509-132c3a21b983";
                        break;

                    case "Papa Johns":
                        storeId = pjStoreId;
                        token = "a82fc67b-aed1-47ce-be86-d678e1f5ea2f";
                        break;

                    case "Wang Hnos":
                        storeId = whStoreId;
                        token = "1f6e1a2e-7b06-4875-81ab-7f672c9cab76";
                        break;

                    case "Don Belisario":
                        storeId = dbStoreId;
                        token = "";
                        break;

                    case "Bembos":
                        storeId = bbStoreId;
                        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkdMT1ZPIiwibmJmIjoxNTM4MDAyNTg0LCJleHAiOjE1MzgwMDI4ODQsImlhdCI6MTUzODAwMjU4NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NjYwNCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTY2MDQifQ.7xYdV44obl0ml-dLcvcLWYX3uOIiRYJfu-2nB02GdSs";
                        break;
                }

                data = "{\"menuUrl\": \"http://190.223.40.173/GLOVO/menu/" + jsonFileName + "\"}";

                httpClient.BaseAddress = new Uri(baseUri + storeId + "/menu");

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", token);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(httpClient.BaseAddress.ToString(), content);
                return response;
            }
        }
    }
}