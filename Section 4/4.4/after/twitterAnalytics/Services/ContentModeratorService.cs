using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TwitterAnalytics.Pocos;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;

namespace TwitterAnalytics.Services
{
    public class ContentModeratorService
    {
        List<string> profaneTerms = new List<string>();

        const string _subscriptionKey = "92346713c52b4d26b368fb7235bbe02d";
        const string _azureBaseUrl = "https://northeurope.api.cognitive.microsoft.com";

        public List<string> DetectProfanity(string sourceText)
        {
            ContentModeratorClient client = 
                new ContentModeratorClient(new Helpers.ApiKeyServiceCredentials
                (_subscriptionKey));

            client.Endpoint = _azureBaseUrl;

            var result = client.TextModeration.ScreenText
                ("text/plain", 
                new MemoryStream(Encoding.UTF8.GetBytes(sourceText)), "eng", true);

            Console.WriteLine($"Original String: {result.OriginalText}");
            Console.WriteLine($"AutoCorrected String: {result.AutoCorrectedText}");


            if (result.Terms != null)
            {
                foreach (var term in result.Terms)
                {
                    profaneTerms.Add(term.Term);
                }
            }
            return profaneTerms;
        }

    }
}
