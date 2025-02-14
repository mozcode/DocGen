using DocGen.Abstract.Interface.Settings;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace DocGen.Pdf.Settings
{
    /// <summary>
    /// PDF'e özgü font ayarları. 
    /// </summary>
    public class PdfFontSettings : IFontSettings
    {
        public string FontColor { get; set; } = "#000000";
        public double FontSize { get; set; } = 12.0;
        public bool FontBold { get; set; }
        public bool FontItalic { get; set; }
        public bool FontUnderline { get; set; }
        public string Justification { get; set; } = "left";

        /// <summary>
        /// iText7'de kullanılacak PdfFont bilgisini döndürmek üzere 
        /// IFontSettings’in ‘ConvertToLibrarySpecificFormat’ metodu.
        /// </summary>
        public object ConvertToLibrarySpecificFormat()
        {
            string baseFont = StandardFonts.HELVETICA;
            if (FontBold && FontItalic)
                baseFont = StandardFonts.HELVETICA_BOLDOBLIQUE;
            else if (FontBold)
                baseFont = StandardFonts.HELVETICA_BOLD;
            else if (FontItalic)
                baseFont = StandardFonts.HELVETICA_OBLIQUE;

            PdfFont pdfFont = PdfFontFactory.CreateFont(baseFont);
            return pdfFont;
        }
    }
}
