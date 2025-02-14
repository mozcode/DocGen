using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.Settings;
using System.Collections.Generic;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Represents any section in a document that may have table rows and a font setting,
    /// plus potential sub-sections.
    /// </summary>
    public interface IDocumentSection
    {
        List<TablesRow> TablesRows { get; }
        IFontSettings? FontSettings { get; set; }
        IEnumerable<IDocumentSection> GetSubSections();
    }
}
