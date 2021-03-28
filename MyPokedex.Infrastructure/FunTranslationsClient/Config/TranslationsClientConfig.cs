namespace MyPokedex.Infrastructure.FunTranslationsClient.Config
{
    using Microsoft.Extensions.Configuration;
    using System;

    public class TranslationsClientConfig : ITranslationsClientConfig
    {
        public TranslationsClientConfig(IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Bind("TranslationsService", this);
        }

        public Uri BaseUri { get; set; }
        public int Timeout { get; set; }
    }
}
