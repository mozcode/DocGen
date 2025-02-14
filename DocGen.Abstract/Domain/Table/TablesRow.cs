using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Table;
using DocGen.Abstract.Interface.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocGen.Abstract.Domain.Table
{
    /// <summary>
    /// Represents a single table row, containing cells and optional column widths.
    /// </summary>
    public class TablesRow
    {
        private const int MaxTotalWidth = 100;

        public TablesRow()
        {
            TableCells = new List<ITableCell>();
            ColumnWidths = new List<int>();
        }

        public List<ITableCell> TableCells { get; set; }
        public List<int> ColumnWidths { get; set; }
        public IFontSettings? FontSettings { get; set; }

        public void ValidateColumnWidths()
        {
            if (ColumnWidths == null || ColumnWidths.Count == 0)
                return;

            int totalWidth = ColumnWidths.Sum();
            if (totalWidth > MaxTotalWidth)
            {
                AdjustColumnWidths(totalWidth);
            }
        }

        private void AdjustColumnWidths(int totalWidth)
        {
            double ratio = (double)MaxTotalWidth / totalWidth;
            for (int i = 0; i < ColumnWidths.Count; i++)
            {
                ColumnWidths[i] = (int)Math.Round(ColumnWidths[i] * ratio);
            }

            int adjustedTotal = ColumnWidths.Sum();
            int difference = adjustedTotal - MaxTotalWidth;
            while (difference > 0)
            {
                int maxIndex = ColumnWidths.IndexOf(ColumnWidths.Max());
                ColumnWidths[maxIndex]--;
                difference--;
            }
        }
    }
}
