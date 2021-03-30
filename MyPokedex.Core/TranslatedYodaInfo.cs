namespace MyPokedex.Core
{
    using Newtonsoft.Json;

    public class TranslatedYodaInfo
    {
        [JsonProperty("contents")]
        public Contents Content { get; set; }
    }
}
