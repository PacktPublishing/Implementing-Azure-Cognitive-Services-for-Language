using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace LookupsAndExamples
{
    class MainClass
    {
        const string _subscriptionKey = "b4d009edae8f42398f2ea893a9f4017d";
        const string _host =
            "https://api.cognitive.microsofttranslator.com";

        public static void Main(string[] args)
        {
            LookupAlternatives("gardens", "en", "fr");
        }

        static void LookupAlternatives(string word, string source, string target)
        {
            string route = 
                $"/dictionary/lookup?api-version=3.0&from={source}&to={target}";

            System.Object[] body = new System.Object[] { new { Text = word } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;

                request.RequestUri = new Uri(_host + route);

                request.Content = new StringContent(requestBody, 
                    Encoding.UTF8, "application/json");

                request.Headers.Add("Ocp-Apim-Subscription-Key", 
                    _subscriptionKey);

                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                var lookupResponse = 
                    JsonConvert.DeserializeObject<List<LookupResponse>>(jsonResponse);

                PrintResults(lookupResponse);
                Console.ReadKey();
            }
        }

        private static void PrintResults(List<LookupResponse> lookupResponse)
        {
    
            foreach (var response in lookupResponse)
            {
                Console.WriteLine($"Original word: {response.DisplaySource}\n");
            

                foreach (var translation in response.Translations)
                {
                    Console.WriteLine("Translation\t\tPosTag\n");
                    Console.WriteLine($"{translation.NormalizedTarget,20}{translation.PosTag,10}");
                    Console.WriteLine();
                    Console.WriteLine($"Back translation\t\tNumber of Examples\n");
                    foreach(var alt in translation.BackTranslations)
                    {
                        Console.WriteLine($"{alt.NormalizedText,20}{alt.NumExamples,10}\n");
                    }
                }
            }
        }
    }
}
