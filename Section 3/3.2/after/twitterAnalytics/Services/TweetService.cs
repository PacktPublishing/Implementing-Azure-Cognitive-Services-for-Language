using System;
using System.Collections.Generic;
using TwitterAnalytics.Helpers;

namespace TwitterAnalytics.Services
{
    public class TweetService
    {
        private TextAnalyticsService textAnalyticsService;

        private TranslatorTextService translatorTextService;

        public TweetService()
        {
            textAnalyticsService = new TextAnalyticsService();
            translatorTextService = new TranslatorTextService();

        }

        internal bool ProcessTweet(string requestBody)
        {
            // This is where we will do our work

            try
            {
                ProcessTranslation(requestBody);

            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        private void ProcessTranslation(string requestBody)
        {
            var detectionResult = translatorTextService.Detect(requestBody);

            if(detectionResult.Language == "en")
            {
                Console.WriteLine($"The recieved Tweet is already in English!");

            }
            else
            {
                Console.WriteLine($"The recieved Tweet is in: {detectionResult.Language}");

                var translation = translatorTextService.Translate(requestBody, "en");

                Console.WriteLine($"Detected Language: {translation.DetectedLanguage.Language}");
                Console.WriteLine($"Language Confidence Score: {translation.DetectedLanguage.Score}");
                Console.WriteLine($"Translated Text: {translation.Translations[0].Text}");
                Console.WriteLine($"Translated Language: {translation.Translations[0].To}");
            }

        }

        private void ProcessDetectEntities(string requestBody)
        {
            var entities = textAnalyticsService.DetectEntities(requestBody);

        }

        private void ProcessKeyPhrases(string requestBody)
        {
            var keyPhrases = textAnalyticsService.DetectKeyPhrases(requestBody);
            foreach(var phrase in keyPhrases)
            {
                Console.WriteLine($"Key Phrase: {phrase}");
            }
        }

        private void ProcessSentiment(string requestBody)
        {
            Sentiment detectedSentiment = textAnalyticsService.DetectSentiment(requestBody);

            switch (detectedSentiment)
            {
                case Sentiment.Positive:
                    Console.WriteLine($"The string {requestBody} has a Positive Sentiment.");
                    break;
                case Sentiment.Negative:
                    Console.WriteLine($"The string {requestBody} has a Negative Sentiment.");
                    break;
                case Sentiment.Neutral:
                    Console.WriteLine($"The string {requestBody} has a Neutral Sentiment.");
                    break;

            }
        }
    }
}
