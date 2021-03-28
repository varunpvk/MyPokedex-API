namespace MyPokedex.Infrastructure.FunTranslationsClient.Config
{
    using System;

    public interface ITranslationsClientConfig
    {
        Uri BaseUri { get; set; }
        int Timeout { get; set; }
    }
}
