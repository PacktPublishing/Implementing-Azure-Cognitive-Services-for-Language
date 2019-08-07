using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using TwitterAnalytics.Helpers;

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
            ITextAnalyticsClient client =
                new TextAnalyticsClient(new Helpers.ApiKeyServiceCredentials
                (_textAnalyticsKey))
                {
                    Endpoint = "https://northeurope.api.cognitive.microsoft.com"
                };

            return client;
        }

        internal string DetectLanguage(string requestBody)
        {
            var client = GetClient();

            LanguageBatchInput input = new LanguageBatchInput()
            {
                Documents = new List<LanguageInput>()
            {
                new LanguageInput()
                {
                Id="1",
                Text=requestBody
                }
            }
            };
            var result = client.DetectLanguageAsync(true, input).Result;

            foreach (var document in result.Documents)
            {
                Console.WriteLine($"Document ID: {document.Id} , " +
                    $"Language: {document.DetectedLanguages[0].Name}," +
                    $" Score: " +
                        $"{document.DetectedLanguages[0].Score}");
            }

            return result.Documents[0].DetectedLanguages[0].Iso6391Name;
        }


        internal Sentiment DetectSentiment(string requestBody)
        {
            var client = GetClient();

            var language = DetectLanguage(requestBody);

            if (language == "(Unknown)")
            {
                throw new ApplicationException("Language is unknown");
            }

            var input = new MultiLanguageBatchInput(
           new List<MultiLanguageInput>()
            {
            new MultiLanguageInput() {
                Id = "1",
                Language = language,
                Text = requestBody }
            });

            var result = client.SentimentAsync(true, input).Result;

            foreach (var document in result.Documents)
            {
                Console.WriteLine($"Document ID: {document.Id} , " +
                    $"Sentiment Score: {document.Score:0.00}," +
                    $" Statistics Character Count: " +
                        $"{document.Statistics.CharactersCount}");
            }

            var sentimentScore = result.Documents[0].Score;

            if (sentimentScore <= 0.3)
            {
                return Sentiment.Negative;
            }
            if (sentimentScore >= 0.7)
            {
                return Sentiment.Positive;
            }
            else
            {
                return Sentiment.Neutral;
            }

        }

        internal ICollection<string> DetectKeyPhrases(string requestBody)
        {
            var client = GetClient();

            var language = DetectLanguage(requestBody);

            if (language == "(Unknown)")
            {
                throw new ApplicationException("Language is unknown");
            }

            var input = new MultiLanguageBatchInput(
          new List<MultiLanguageInput>()
           {
            new MultiLanguageInput() {
                Id = "1",
                Language = language,
                Text = requestBody }
           });

            var result = client.KeyPhrasesAsync(true, input).Result;

            foreach (var document in result.Documents)
            {
                Console.WriteLine($"Document ID: {document.Id} , " +
                    $"Number of KeyPhrases: {document.KeyPhrases.Count}," +
                    $" Statistics Character Count: " +
                        $"{document.Statistics.CharactersCount}");
            }
            return result.Documents[0].KeyPhrases;
        }

        internal ICollection<string> DetectEntities(string requestBody)
        {
            var client = GetClient();

            var language = DetectLanguage(requestBody);

            if (language == "(Unknown)")
            {
                throw new ApplicationException("Language is unknown");
            }

            var input = new MultiLanguageBatchInput(
          new List<MultiLanguageInput>()
           {
            new MultiLanguageInput() {
                Id = "1",
                Language = language,
                Text = requestBody }
           });

            var result = client.EntitiesAsync(true, input).Result;

            List<string> entitiesResult = new List<string>();

            foreach (var document in result.Documents)
            {
                foreach (var entity in document.Entities)
                {
                    entitiesResult.Add(entity.Name);

                   
                    Console.WriteLine($"Entity Name: {entity.Name}");
                    if (!string.IsNullOrEmpty(entity.SubType))
                    {
                        Console.WriteLine($"Type : {entity.SubType}");
                    }
                    if (!string.IsNullOrEmpty(entity.WikipediaUrl))
                    {
                        Console.WriteLine($"Type : {entity.WikipediaUrl}");
                    }
                    if (!string.IsNullOrEmpty(entity.WikipediaLanguage))
                    {
                        Console.WriteLine($"Wikipedia Language : {entity.WikipediaLanguage}");
                    }
                }
            }

            return entitiesResult;

        }

    }
}
