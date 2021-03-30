using Newtonsoft.Json;

namespace MyPokedex.Core
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
