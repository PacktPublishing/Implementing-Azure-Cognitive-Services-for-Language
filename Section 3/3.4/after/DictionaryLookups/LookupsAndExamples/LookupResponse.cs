using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class LookupResponse {
    [JsonProperty("displaySource")] public string DisplaySource

{
    get;
    set;
}

[JsonProperty("normalizedSource")]
public string NormalizedSource {
    get;
    set;
}

[JsonProperty("translations")]
public Translation[] Translations {
    get;
    set;
}

}

public partial class Translation {
    [JsonProperty("backTranslations")] public BackTranslation[] BackTranslations

{
    get;
    set;
}

[JsonProperty("confidence")]
public long Confidence {
    get;
    set;
}

[JsonProperty("displayTarget")]
public string DisplayTarget {
    get;
    set;
}

[JsonProperty("normalizedTarget")]
public string NormalizedTarget {
    get;
    set;
}

[JsonProperty("posTag")]
public string PosTag {
    get;
    set;
}

[JsonProperty("prefixWord")]
public string PrefixWord {
    get;
    set;
}

}

public partial class BackTranslation {
    [JsonProperty("displayText")] public string DisplayText

{
    get;
    set;
}

[JsonProperty("frequencyCount")]
public long FrequencyCount {
    get;
    set;
}

[JsonProperty("normalizedText")]
public string NormalizedText {
    get;
    set;
}

[JsonProperty("numExamples")]
public long NumExamples {
    get;
    set;
}

}

public partial class LookupResponse {
    public static LookupResponse[] FromJson(string json) => JsonConvert.DeserializeObject<LookupResponse[]>(json, Converter.Settings);
}

public static class Serialize {
    public static string ToJson(this LookupResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal static class Converter {
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings

{
    MetadataPropertyHandling = MetadataPropertyHandling.Ignore, DateParseHandling = DateParseHandling.None, Converters =

{
    new IsoDateTimeConverter

{
    DateTimeStyles = DateTimeStyles.AssumeUniversal
}

}
,
}
;
}
