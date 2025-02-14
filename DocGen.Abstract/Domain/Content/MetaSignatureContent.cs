using System.Collections.Generic;
using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Domain.Content
{
    /// <summary>
    /// Represents the signature portion of a document,
    /// storing table rows and optional font settings.
    /// Implements IDocumentSection for usage in DocumentContentInfoProvider.
    /// 
    /// "PlaceAtBottom" özelliği ile çıktı üretim katmanı,
    /// sayfanın en altına yerleştirebiliyor.
    /// </summary>
    public class MetaSignatureContent : SubContent, IDocumentSection
    {
        public MetaSignatureContent(List<TablesRow> tablesRows, IFontSettings? fontSettings = null)
        {
            this.TablesRows = tablesRows;
            this.FontSettings = fontSettings;
        }

        public bool PlaceAtBottom { get; set; } = true;

        public new List<TablesRow> TablesRows
        {
            get => base.TablesRows;
            set => base.TablesRows = value;
        }

        public new IFontSettings? FontSettings
        {
            get => base.FontSettings;
            set => base.FontSettings = value;
        }

        public IEnumerable<IDocumentSection> GetSubSections()
        {
            return new List<IDocumentSection>();
        }
    }
}
