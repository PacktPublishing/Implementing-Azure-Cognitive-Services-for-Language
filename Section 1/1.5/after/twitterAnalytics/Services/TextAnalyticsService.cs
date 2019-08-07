using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;

namespace TwitterAnalytics.Services
{
    public class TextAnalyticsService
    {
        private const string _textAnalyticsKey = "a8a23a3946c14e828eb87907ff6ce72f";

        public TextAnalyticsService()
        {
        }

        private ITextAnalyticsClient GetClient()
        {
            ITextAnalyticsClient client = new TextAnalyticsClient(new Helpers.ApiKeyServiceCredentials(_textAnalyticsKey))
            {
                Endpoint = "https://northeurope.api.cognitive.microsoft.com"
            };

            return client;
        }

internal string DetectLanguage(string requestBody)
        {
            throw new NotImplementedException();
        }

        internal double DetectSentiment(string requestBody)
        {
            throw new NotImplementedException();
        }

        internal ICollection<string> DetectKeyPhrases(string requestBody)
        {
            throw new NotImplementedException();
        }

        internal ICollection<string> DetectEntities(string requestBody)
        {
            throw new NotImplementedException();
        }

    }
}
