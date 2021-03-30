namespace MyPokedex.Infrastructure.PokeAPIClient.Config
{
    using Microsoft.Extensions.Configuration;
    using System;

    public class PokeApiClientConfig : IPokeApiClientConfig
    {
        public PokeApiClientConfig(IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Bind("PokeService", this);
        }
        public Uri BaseUri { get; set; }
        public int Timeout { get; set; }
    }
}
