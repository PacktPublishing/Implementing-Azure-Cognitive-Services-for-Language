using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TwitterAnalytics.Pocos
{
    public partial class DetectRepsonse
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("isTranslationSupported")]
        public bool IsTranslationSupported { get; set; }

        [JsonProperty("isTransliterationSupported")]
        public bool IsTransliterationSupported { get; set; }

        [JsonProperty("alternatives", NullValueHandling = NullValueHandling.Ignore)]
        public DetectRepsonse[] Alternatives { get; set; }
    }

    public partial class DetectRepsonse
    {
        public static DetectRepsonse[] FromJson(string json) => JsonConvert.DeserializeObject<DetectRepsonse[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DetectRepsonse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}




