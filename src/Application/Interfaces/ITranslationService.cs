namespace NutriTrack.src.Application.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateToEnglishAsync(string text);
    }
}
