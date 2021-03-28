namespace MyPokedex.Infrastructure.FunTranslationsClient
{
    using MyPokedex.Core;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITranslationsService
    {
        Task<TranslatedShakespheareInfo> GetShakespheareTranslationAsync(string inputText, CancellationToken cancellationToken);
        Task<TranslatedYodaInfo> GetYodaTranslationAsync(string inputText, CancellationToken cancellationToken);
    }
}
