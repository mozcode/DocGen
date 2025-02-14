using System.Collections.Generic;
using System.Linq;
using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Interface.Settings;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Table;
using DocGen.Abstract.Domain.Table;

namespace DocGen.Abstract.Interface.Content.Concrete
{
    /// <summary>
    /// Represents the body part of a document, with its sections and font settings.
    /// Also implements IDocumentSection so it can be returned as an IDocumentSection.
    /// </summary>
    public class BodyContent : IDocumentSection, IBodyContent
    {
        private IFontSettings _textFontSettings;
        private IFontSettings _headerFontSettings;
        private List<IBodySection> _bodySections;

        public IFontSettings TextFontSettings
        {
            get => _textFontSettings;
            set => _textFontSettings = value;
        }

        public IFontSettings HeaderFontSettings
        {
            get => _headerFontSettings;
            set => _headerFontSettings = value;
        }

        public List<IBodySection> BodySections
        {
            get => _bodySections;
            set => _bodySections = value;
        }

        public BodyContent()
        {
            _textFontSettings = null!;
            _headerFontSettings = null!;
            _bodySections = new List<IBodySection>();
        }

        public BodyContent(
            IFontSettingsFactory fontSettingsFactory,
            List<IBodySection> bodySections,
            IFontSettings headerFontSettings,
            IFontSettings? textFontSettings = null)
        {
            _textFontSettings = textFontSettings ?? fontSettingsFactory.CreateFontSettings();
            _headerFontSettings = headerFontSettings ?? fontSettingsFactory.CreateFontSettings();
            _bodySections = bodySections;
        }

        public void AddBodySection(IBodySection bodySection)
        {
            _bodySections.Add(bodySection);
        }

        // IDocumentSection implementation
        public List<TablesRow> TablesRows
        {
            get
            {
                // BodyContent seviyesinde tablo kullanılmıyorsa, boş dönebiliriz.
                return new List<TablesRow>();
            }
        }

        public IFontSettings? FontSettings
        {
            get => _textFontSettings;
            set => _textFontSettings = value ?? _textFontSettings;
        }

        public IEnumerable<IDocumentSection> GetSubSections()
        {
            // Eğer BodySection da IDocumentSection uygulasaydı, 
            // alt bölümleri buradan dönebilirdik.
            return new List<IDocumentSection>();
        }
    }
}
