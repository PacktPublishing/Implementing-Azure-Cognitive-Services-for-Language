using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;

namespace ImageModeration
{
    class MainClass
    {
        const string _subscriptionKey = "92346713c52b4d26b368fb7235bbe02d";
        const string _azureBaseUrl = 
            "https://northeurope.api.cognitive.microsoft.com";

        public static void Main(string[] args)
        {
            string url = "https://moderatorsampleimages.blob.core.windows.net/samples/sample.jpg";

            var imageText = GetImageText(url);

            Console.WriteLine(imageText);

            var moderationResult = ModerateText(imageText);


            if ((moderationResult != null) && moderationResult.Any())
            {
                Console.WriteLine("Profanity Detected!");
                foreach (var term in moderationResult)
                {
                    Console.WriteLine(term);
                }
            }
            else
            {
                Console.WriteLine("No Profanity Detected!");
            }
            Console.ReadKey();
        }

        private static string GetImageText(string url)
        {
            ContentModeratorClient client =
                 new ContentModeratorClient(new ApiKeyServiceCredentials
                 (_subscriptionKey))
                 {
                     Endpoint = _azureBaseUrl
                 };

            var result = client.ImageModeration.OCRFileInput("eng",
              new MemoryStream(LoadImage(url)));

            return result.Text;
        }

        private static IList<DetectedTerms> ModerateText(string text)
        {
            ContentModeratorClient client =
               new ContentModeratorClient(new ApiKeyServiceCredentials
               (_subscriptionKey))
               {
                   Endpoint = _azureBaseUrl
               };

            var result = client.TextModeration.ScreenText
               ("text/plain",
               new MemoryStream(Encoding.UTF8.GetBytes(text)), "eng", true);
            return result.Terms;
        }

        private static byte[] LoadImage(string url)
        {
            try
            {
                WebClient client = new WebClient();
                byte[] result = client.DownloadData(url);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
