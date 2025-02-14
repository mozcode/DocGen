using System.Collections.Generic;
using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Domain.Content
{
    /// <summary>
    /// Base class for sections that contain tables (header, footer, signature).
    /// </summary>
    public class SubContent : ISubContent
    {
        public SubContent()
        {
            TablesRows = new List<TablesRow>();
        }

        public List<TablesRow> TablesRows { get; set; }
        public IFontSettings? FontSettings { get; set; }

        /// <summary>
        /// Adds a row to the subcontent's table list.
        /// </summary>
        public void AddRow(TablesRow tablesRow)
        {
            TablesRows.Add(tablesRow);
        }
    }
}
