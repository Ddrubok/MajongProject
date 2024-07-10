using AccountServer.Data;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AccountServer.Services
{
    public class FacebookService
    {
        HttpClient _httpClient;

        readonly string _accessToken = "EAAYOKYe3YgEBOxIdrgAHMx1OhaUeqjZC35sHC27gJYZCAQWMhHZChuHGLf9A1k0SUIuAqpISeA2I6f6KMU2ZAPvcZCgaiOvgcHFKaKoh9muiQqCbv7XDeKqqL8sRKZBlRqPKXcOXXtv477smrEu5XFOZBP2ISCRLwlP8VZCLPU6Afqp4SEiC3nSZBRICsTJ2vqCL0CZAIbQYZCzpB1Rk5soUwLZBoqZASPZA7mK8thiPUSHSdbtJsPjcqaiSnYxn2h0lm7qwZDZD"; // 바꿔

        public FacebookService()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://graph.facebook.com/") };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<FacebookTokenData?> GetUserTokenData(string inputToken)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"debug_token?input_token={inputToken}&access_token={_accessToken}");

            if (!response.IsSuccessStatusCode)
                return null;

            string resultStr = await response.Content.ReadAsStringAsync();

            FacebookResponseJsonData? result = JsonConvert.DeserializeObject<FacebookResponseJsonData>(resultStr);
            if (result == null)
                return null;

            return result.data;
        }
    }
}
