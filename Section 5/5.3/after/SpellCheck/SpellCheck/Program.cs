using System;
using System.Text;
using Microsoft.Azure.CognitiveServices.Language.SpellCheck;
using Microsoft.Azure.CognitiveServices.Language.SpellCheck.Models;

namespace SpellCheck
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string subscriptionKey = "c66350565d054032b93fdb323939d884";

            string sourceString = "james had a very good day at at work he wass so plaesed with himslf";

            SpellCheckClient client = new SpellCheckClient(new ApiKeyServiceClientCredentials(subscriptionKey));
           var spellResult = client.SpellCheckerAsync(sourceString, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "proof", null, null).Result;

            foreach (var token in spellResult.FlaggedTokens)
            {
                Console.WriteLine(PrettyToken(token));
            }

            Console.WriteLine("Finished");
            Console.ReadKey();

        }

        private static string PrettyToken(SpellingFlaggedToken token)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Token: {token.Token}");
            builder.AppendLine($"Offset: {token.Offset}");
            builder.AppendLine($"Type: {token.Type}");
            builder.AppendLine("** Sugestions **");
            foreach(var sugestion in token.Suggestions)
            {
                builder.AppendLine($"Sugestion: {sugestion.Suggestion}");
                builder.AppendLine($"Score: {sugestion.Score}");

            }
            return builder.ToString();


        }
    }
}
