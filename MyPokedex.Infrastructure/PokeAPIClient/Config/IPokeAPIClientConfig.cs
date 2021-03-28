namespace MyPokedex.Infrastructure.PokeAPIClient.Config
{
    using System;
    public interface IPokeApiClientConfig
    {
        Uri BaseUri { get; set; }
        int Timeout { get; set; }
    }
}
