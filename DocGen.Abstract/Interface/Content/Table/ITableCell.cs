using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Interface.Content.Table
{
    /// <summary>
    /// Represents a generic table cell in the document.
    /// </summary>
    public interface ITableCell
    {
        int RowNumber { get; set; }
        int ColumnNumber { get; set; }
        IFontSettings? FontSettings { get; set; }
    }
}
