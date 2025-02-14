using iText.Kernel.Colors;
using iText.Layout.Properties;
using System.Drawing;
using DocGen.Abstract.Constants;

namespace DocGen.Pdf.Utility
{
    /// <summary>
    /// Helper methods for iText PDF conversions (color, justification).
    /// </summary>
    public static class PdfHelper
    {
        public static DeviceRgb ConvertHexToColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor) || !hexColor.StartsWith("#"))
                return new DeviceRgb(0, 0, 0);

            var drawingColor = ColorTranslator.FromHtml(hexColor);
            return new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B);
        }

        public static TextAlignment ConvertJustification(string justificationName = "left")
        {
            return justificationName.ToLower() switch
            {
                Justifications.Left => TextAlignment.LEFT,
                Justifications.Center => TextAlignment.CENTER,
                Justifications.Right => TextAlignment.RIGHT,
                Justifications.Justified => TextAlignment.JUSTIFIED,
                _ => TextAlignment.LEFT
            };
        }
    }
}
