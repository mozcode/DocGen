using DocGen.Abstract.Settings.Concrete;
using DocumentFormat.OpenXml.Wordprocessing;
using DocGen.Word.Utility;
using System.Drawing;

namespace DocGen.Word.Settings
{
    /// <summary>
    /// Font settings specialized for OpenXML Word rendering.
    /// </summary>
    public class WordFontSettings : FontSettings
    {
        public override object ConvertToLibrarySpecificFormat()
        {
            var runProperties = new RunProperties
            {
                RunFonts = new RunFonts { Ascii = "Arial" },
                Color = new Color { Val = FontColor.TrimStart('#') },
                FontSize = new FontSize { Val = (FontSize * 2).ToString() },
                Bold = FontBold ? new Bold() : null,
                Italic = FontItalic ? new Italic() : null,
                Underline = FontUnderline ? new Underline() : null
            };

            var justification = WordHelper.ConvertJustification(Justification);
            var paragraphProperties = new ParagraphProperties
            {
                Justification = justification
            };

            // Burada bir anonim obje döndürüyoruz
            return new { RunProps = runProperties, ParaProps = paragraphProperties };
        }
    }
}
