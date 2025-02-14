using DocGen.Abstract.Interface.Content.Table;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Base class for table cells that implements ITableCell interface.
    /// </summary>
    public abstract class BaseTableCell : ITableCell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public IFontSettings? FontSettings { get; set; }
    }
}
