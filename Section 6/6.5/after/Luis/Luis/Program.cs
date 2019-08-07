using System;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;

namespace Luis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var key = "13ba3bfb329b48d98645c4519d8b39b8";

            var luisClient = new LUISRuntimeClient
                (new ApiKeyServiceClientCredentials(key), 
                new System.Net.Http.DelegatingHandler[] { });
            luisClient.Endpoint = "https://westus.api.cognitive.microsoft.com";

            string appId = "7998ce4d-827c-44b7-a0f7-2632d06252a4";

            while (true)
            {
                Console.WriteLine("Enter some text...");
                var input = Console.ReadLine();
                var prediction = new Prediction(luisClient);
                var result = prediction.ResolveAsync(appId, input).Result;

                switch (result.TopScoringIntent.Intent)
                {
                    case "GetJobInfo":
                        Console.WriteLine($"Matches the GetJobInfo intent with a score of {result.TopScoringIntent.Score}");
                        break;
                    case "ApplyForJob":
                        Console.WriteLine($"Matches the ApplyForJob intent with a score of {result.TopScoringIntent.Score}");
                        break;
                    default:
                        Console.WriteLine($"Matches the None intent with a score of {result.TopScoringIntent.Score}");
                        break;
                }

            }
        }
    }
}
