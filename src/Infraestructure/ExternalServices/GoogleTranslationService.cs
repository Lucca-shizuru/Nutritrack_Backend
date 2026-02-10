using GTranslate.Translators;
using NutriTrack.src.Application.Interfaces;

namespace NutriTrack.src.Infraestructure.ExternalServices
{
    public class GoogleTranslationService : ITranslationService
    {

        private readonly AggregateTranslator _translator;

        public GoogleTranslationService()
        {
           
            _translator = new AggregateTranslator();
        }
        public async Task<string> TranslateToEnglishAsync(string text)
        {
            var result = await _translator.TranslateAsync(text, "en");
            return result.Translation;
        }
    }
}
