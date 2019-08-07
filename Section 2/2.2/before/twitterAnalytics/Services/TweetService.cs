using System;
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
                Sentiment detectedSentiment = 
                     service.DetectSentiment(requestBody);

                switch (detectedSentiment)
                {
                    case Sentiment.Positive:
                        Console.WriteLine( $"The string {requestBody} has a Positive Sentiment.");
                        break;
                    case Sentiment.Negetive:
                        Console.WriteLine($"The string {requestBody} has a Negative Sentiment.");
                        break;
                    case Sentiment.Neutral:
                        Console.WriteLine($"The string {requestBody} has a Neautral Sentiment.");
                        break;

                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
    }
}
