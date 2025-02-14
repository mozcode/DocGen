using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Word.FactorySettings
{
    public class WordFontSettingsFactory : IFontSettingsFactory
    {
        private readonly IFontSettings _baseSettings;

        public WordFontSettingsFactory(IFontSettings baseSettings)
        {
            _baseSettings = baseSettings;
        }

        public IFontSettings CreateFontSettings()
        {
            return new Settings.WordFontSettings
            {
                FontColor = _baseSettings.FontColor,
                FontSize = _baseSettings.FontSize,
                FontBold = _baseSettings.FontBold,
                FontItalic = _baseSettings.FontItalic,
                FontUnderline = _baseSettings.FontUnderline,
                Justification = _baseSettings.Justification
            };
        }
    }
}
