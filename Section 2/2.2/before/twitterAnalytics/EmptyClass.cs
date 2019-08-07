var client = GetClient();
var input = new MultiLanguageBatchInput(
    new List<MultiLanguageInput>()
{
            new MultiLanguageInput() {
                Id = "1",
                Language = "en",
                Text = requestBody }
});

var result = client.SentimentAsync(true, input).Result;

            foreach (var document in result.Documents)
            {
                Console.WriteLine($"Document ID: {document.Id} ," +
                    " Sentiment Score: {document.Score:0.00}," +
                    $" Statistics Character Count: " +
                        "{document.Statistics.CharactersCount}");
            }

            var sentimentScore = result.Documents[0].Score;

            if (sentimentScore <= 0.3)
            {
                return Sentiment.Negetive;
            }
            if (sentimentScore >= 0.7)
            {
                return Sentiment.Positive;
            }
            else
            {
                return Sentiment.Neutral;
            }