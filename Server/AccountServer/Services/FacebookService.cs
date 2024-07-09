using AccountServer.Data;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AccountServer.Services
{
    public class FacebookService
    {
        HttpClient _httpClient;

        readonly string _accessToken = "0"; // 바꿔

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
