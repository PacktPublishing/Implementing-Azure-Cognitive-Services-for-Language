using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TwitterAnalytics.Pocos;

namespace TwitterAnalytics.Services
{
    public class TranslatorTextService
    {
        const string _subscriptionKey = "b4d009edae8f42398f2ea893a9f4017d";
        const string _host =
            "https://api.cognitive.microsofttranslator.com";
       

        public DetectRepsonse Detect(string sourceText)
        {
            var route = "/detect?api-version=3.0";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;

                request.RequestUri = new Uri(_host + route);

                System.Object[] body = new System.Object[] { new { Text = sourceText } };
                var requestBody = JsonConvert.SerializeObject(body);

                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

                var response = client.SendAsync(request).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

               List<DetectRepsonse> detectResponse = JsonConvert.DeserializeObject<List<DetectRepsonse>>(responseString);

                return detectResponse[0];
            }
        }

        public TranslationResponse Translate(string sourceText,
                                      string targetLanguage)
        {
            var route = "/translate?api-version=3.0&to={0}";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;

                request.RequestUri = new Uri(string.Format(_host+route, targetLanguage));

                System.Object[] body = new System.Object[] { new { Text = sourceText } };
                var requestBody = JsonConvert.SerializeObject(body);

                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

                var response = client.SendAsync(request).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                List<TranslationResponse> translationResponse = JsonConvert.DeserializeObject<List<TranslationResponse>>(responseString);

                return translationResponse[0];

            }
        }


    }
}
