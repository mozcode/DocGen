using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Settings.Concrete
{
    /// <summary>
    /// Base font settings that can implement IFontSettings.
    /// PDF/Word classes can derive from or override this if needed.
    /// </summary>
    public class FontSettings : IFontSettings
    {
        public string FontColor { get; set; } = "#000000";
        public double FontSize { get; set; } = 12.0;
        public bool FontBold { get; set; }
        public bool FontItalic { get; set; }
        public bool FontUnderline { get; set; }
        public string Justification { get; set; } = "left";

        public virtual object ConvertToLibrarySpecificFormat()
        {
            // Return a generic object or do nothing in base.
            return new object();
        }
    }
}
