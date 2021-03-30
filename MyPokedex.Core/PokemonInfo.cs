namespace MyPokedex.Core
{
    using Newtonsoft.Json;
    public class PokemonInfo
    {
        [JsonProperty("flavor_text_entries")]
        public FlavorTextEntries[] FlavorTextEntries { get; set; }
        [JsonProperty("habitat")]
        public Habitat Habitat { get; set; }
        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    
    public class Habitat
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FlavorTextEntries
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }
        [JsonProperty("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
