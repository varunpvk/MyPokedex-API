namespace MyPokedex.Core
{
    using Newtonsoft.Json;

    public class TranslatedYodaInfo
    {
        public class TranslatedShakespheareInfo
        {
            [JsonProperty("contents")]
            public Contents Content { get; set; }
        }

        public class Contents
        {
            [JsonProperty("translated")]
            public string TranslatedText { get; set; }

            [JsonProperty("text")]
            public string OriginalText { get; set; }
        }
    }
}
