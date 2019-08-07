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

        const string _subscriptionKey = "c76af513a9e7423b90633c5c4cacf47e";
        const string _azureBaseUrl = "https://uksouth.api.cognitive.microsoft.com";

        public List<string> DetectProfanity(string sourceText)
        {
            ContentModeratorClient client = new ContentModeratorClient(new Helpers.ApiKeyServiceCredentials
                (_subscriptionKey));

            client.Endpoint = _azureBaseUrl;

            var result = client.TextModeration.ScreenText("text/plain", new MemoryStream(Encoding.UTF8.GetBytes(sourceText)), "eng");

            foreach (var term in result.Terms)
            {
                profaneTerms.Add(term.Term);
            }
            return profaneTerms;
        }

    }
}
