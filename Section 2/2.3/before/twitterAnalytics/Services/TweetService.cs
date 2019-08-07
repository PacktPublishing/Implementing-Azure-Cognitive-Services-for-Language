using System;
using System.Collections.Generic;
using TwitterAnalytics.Helpers;

namespace TwitterAnalytics.Services
{
    public class TweetService
    {
        private TextAnalyticsService service;
        public TweetService()
        {
            service = new TextAnalyticsService();

        }

        internal bool ProcessTweet(string requestBody)
        {
            // This is where we will do our work

            try
            {               
                ProcessKeyPhrases(requestBody);

            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        private void ProcessKeyPhrases(string requestBody)
        {
            var keyPhrases = service.DetectKeyPhrases(requestBody);
            foreach(var phrase in keyPhrases)
            {
                Console.WriteLine($"Key Phrase: {phrase}");
            }
        }

        private void ProcessSentiment(string requestBody)
        {
            Sentiment detectedSentiment = service.DetectSentiment(requestBody);

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
