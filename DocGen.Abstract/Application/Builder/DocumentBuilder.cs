using System.Collections.Generic;
using DocGen.Abstract.Constants;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Concrete;
using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Interface.Settings;
using DocGen.Abstract.Domain.Content;
using DocGen.Abstract.Domain.Table;

namespace DocGen.Abstract.Application.Builder
{
    /// <summary>
    /// An example builder for creating a DocumentContent object in a fluent manner.
    /// 
    /// Kullanımı:
    /// var builder = new DocumentBuilder(_pdfFontSettingsFactory);
    /// builder
    ///     .Header(h => {
    ///         h.AddRow(...);
    ///     })
    ///     .BodySection("Başlık", "içerik..", ...)
    ///     .Footer(f => {
    ///         f.AddRow(...);
    ///     });
    /// var docContent = builder.Build();
    /// 
    /// docCreator.CreateDocument(docContent, new DocumentCreateOptions { ... });
    /// </summary>
    public class DocumentBuilder
    {
        private readonly IFontSettingsFactory _fontSettingsFactory;

        private List<TablesRow> _headerRows = new List<TablesRow>();
        private List<TablesRow> _footerRows = new List<TablesRow>();
        private List<TablesRow> _signatureRows = new List<TablesRow>();
        private List<IBodySection> _bodySections = new List<IBodySection>();

        private IFontSettings? _defaultFontSettings;

        public DocumentBuilder(IFontSettingsFactory fontSettingsFactory)
        {
            _fontSettingsFactory = fontSettingsFactory;
        }

        public DocumentBuilder SetDefaultFontSettings(IFontSettings fontSettings)
        {
            _defaultFontSettings = fontSettings;
            return this;
        }

        public DocumentBuilder Header(System.Action<ISubContent> headerConfig)
        {
            var headerContent = new Domain.Content.MetaHeaderContent(_headerRows);
            headerConfig(headerContent);
            // Sonrasında tablo satırları _headerRows'da oluşacak
            return this;
        }

        public DocumentBuilder Footer(System.Action<ISubContent> footerConfig)
        {
            var footerContent = new Domain.Content.MetaFooterContent(_footerRows);
            footerConfig(footerContent);
            return this;
        }

        public DocumentBuilder Signature(System.Action<ISubContent> signatureConfig)
        {
            var signatureContent = new Domain.Content.MetaSignatureContent(_signatureRows);
            signatureConfig(signatureContent);
            return this;
        }

        public DocumentBuilder BodySection(
            string title,
            string richTextContent,
            int hierarchyLevel = 1,
            List<string>? titleNotes = null,
            List<string>? sectionNotes = null,
            TitleNumberingType numberingType = TitleNumberingType.Numeric,
            System.Action<BodySection>? configure = null)
        {
            var bodySec = new BodySection(
                fontSettingsFactory: _fontSettingsFactory,
                title: title,
                titleNotes: titleNotes ?? new List<string>(),
                sectionNotes: sectionNotes ?? new List<string>(),
                richTextContent: richTextContent,
                hierarchyLevel: hierarchyLevel,
                subBodySections: new List<IBodySection>(),
                numberingType: numberingType
            );
            configure?.Invoke(bodySec);
            _bodySections.Add(bodySec);

            return this;
        }

        public IDocumentContent Build()
        {
            // Varsayılan font ataması
            IFontSettings defFont = _defaultFontSettings
                ?? _fontSettingsFactory.CreateFontSettings();

            var body = new BodyContent(
                fontSettingsFactory: _fontSettingsFactory,
                bodySections: _bodySections,
                headerFontSettings: _fontSettingsFactory.CreateFontSettings(),
                textFontSettings: _fontSettingsFactory.CreateFontSettings()
            );

            var header = new MetaHeaderContent(_headerRows, _fontSettingsFactory.CreateFontSettings());
            var footer = new MetaFooterContent(_footerRows, _fontSettingsFactory.CreateFontSettings());
            var signature = new MetaSignatureContent(_signatureRows, _fontSettingsFactory.CreateFontSettings());

            return new DocumentContent(body, header, footer, signature, defFont);
        }
    }
}
