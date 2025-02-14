using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Pdf.FactorySettings
{
    /// <summary>
    /// Örnek bir fabrika: 
    /// dışarıdan gelen IFontSettings örneğini alıp 
    /// PdfFontSettings'e dönüştürür.
    /// </summary>
    public class PdfFontSettingsFactory : IFontSettingsFactory
    {
        private readonly IFontSettings _baseFontSettings;

        public PdfFontSettingsFactory(IFontSettings baseFontSettings)
        {
            _baseFontSettings = baseFontSettings;
        }

        public IFontSettings CreateFontSettings()
        {
            return new Settings.PdfFontSettings
            {
                FontColor = _baseFontSettings.FontColor,
                FontSize = _baseFontSettings.FontSize,
                FontBold = _baseFontSettings.FontBold,
                FontItalic = _baseFontSettings.FontItalic,
                FontUnderline = _baseFontSettings.FontUnderline,
                Justification = _baseFontSettings.Justification
            };
        }
    }
}
