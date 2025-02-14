using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.Settings;
using System.Collections.Generic;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// A subcontent unit that can hold table rows and an optional font setting.
    /// Typically used by header, footer, or signature sections.
    /// </summary>
    public interface ISubContent
    {
        List<TablesRow> TablesRows { get; set; }
        IFontSettings? FontSettings { get; set; }
        void AddRow(TablesRow tablesRow);
    }
}
