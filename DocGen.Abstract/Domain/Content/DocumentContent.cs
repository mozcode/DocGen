using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Concrete;
using DocGen.Abstract.Interface.Settings;
using DocGen.Abstract.Settings.Concrete;

namespace DocGen.Abstract.Domain.Content
{
    /// <summary>
    /// Aggregates body, header, footer, signature + a default font setting.
    /// Provides a method to merge override font settings.
    /// </summary>
    public class DocumentContent : IDocumentContent
    {
        public BodyContent BodyContent { get; private set; }
        public MetaHeaderContent MetaHeaderContent { get; private set; }
        public MetaFooterContent MetaFooterContent { get; private set; }
        public MetaSignatureContent MetaSignatureContent { get; private set; }
        public IFontSettings DefaultFontSettings { get; private set; }

        public DocumentContent(
            BodyContent bodyContent,
            MetaHeaderContent metaHeaderContent,
            MetaFooterContent metaFooterContent,
            MetaSignatureContent metaSignatureContent,
            IFontSettings defaultFontSettings)
        {
            BodyContent = bodyContent;
            MetaHeaderContent = metaHeaderContent;
            MetaFooterContent = metaFooterContent;
            MetaSignatureContent = metaSignatureContent;
            DefaultFontSettings = defaultFontSettings;
        }

        public IFontSettings GetMergedFontSettings(IFontSettings? overrideSettings)
        {
            if (overrideSettings == null)
                return DefaultFontSettings;

            // Basit property-bazlı merge
            var merged = new FontSettings
            {
                FontColor = string.IsNullOrEmpty(overrideSettings.FontColor)
                    ? DefaultFontSettings.FontColor
                    : overrideSettings.FontColor,
                FontSize = overrideSettings.FontSize <= 0
                    ? DefaultFontSettings.FontSize
                    : overrideSettings.FontSize,
                FontBold = overrideSettings.FontBold,
                FontItalic = overrideSettings.FontItalic,
                FontUnderline = overrideSettings.FontUnderline,
                Justification = string.IsNullOrEmpty(overrideSettings.Justification)
                    ? DefaultFontSettings.Justification
                    : overrideSettings.Justification
            };

            return merged;
        }
    }
}
