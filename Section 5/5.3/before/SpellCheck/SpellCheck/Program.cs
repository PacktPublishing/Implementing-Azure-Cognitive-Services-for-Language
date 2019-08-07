using System;
using System.Text;

namespace SpellCheck
{
    class MainClass
    {
        public static void Main(string[] args)
        {
           
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
