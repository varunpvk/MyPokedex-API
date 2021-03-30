namespace MyPokedex.Infrastructure.FunTranslationsClient
{
    using MyPokedex.Core;
    using System.Threading.Tasks;

    public interface ITranslationsService
    {
        Task<TranslatedShakespheareInfo> GetShakespheareTranslationAsync(string inputText);
        Task<TranslatedYodaInfo> GetYodaTranslationAsync(string inputText);
    }
}
