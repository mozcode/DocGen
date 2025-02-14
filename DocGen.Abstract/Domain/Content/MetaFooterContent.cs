using System.Collections.Generic;
using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Domain.Content
{
    /// <summary>
    /// Represents the footer portion of a document,
    /// storing table rows and optional font settings.
    /// Implements IDocumentSection for usage in DocumentContentInfoProvider.
    /// </summary>
    public class MetaFooterContent : SubContent, IDocumentSection
    {
        public MetaFooterContent(List<TablesRow> tablesRows, IFontSettings? fontSettings = null)
        {
            TablesRows = tablesRows;
            FontSettings = fontSettings;
        }

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
