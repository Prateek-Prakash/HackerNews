using System;
using System.Net.Http;

namespace HackerNews.Clone.API
{
    public static class HTTPClient
    {
        private static HttpClient httpClient = new HttpClient();
        private static bool baseAddressSet = false;

        public static string Get(string requestURL)
        {
            if (!baseAddressSet)
            {
                httpClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
                baseAddressSet = true;
            }
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(requestURL).Result;
            return httpResponseMessage.Content.ReadAsStringAsync().Result;
        }
    }
}
