using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TwitterAnalytics.Pocos
{
    public partial class TranslationResponse
    {
        [JsonProperty("detectedLanguage")]
        public DetectedLanguage DetectedLanguage { get; set; }

        [JsonProperty("translations")]
        public Translation[] Translations { get; set; }
    }

    public partial class DetectedLanguage
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }
    }

    public partial class Translation
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    public partial class TranslationResponse
    {
        public static TranslationResponse[] FromJson(string json) => JsonConvert.DeserializeObject<TranslationResponse[]>(json, Converter.Settings);
    }
   
}
