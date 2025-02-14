namespace DocGen.Abstract.Interface.Settings
{
    /// <summary>
    /// Font settings contract for color, size, bold, italic, etc.
    /// Also requires a method to convert to library-specific format (PDF, Word).
    /// </summary>
    public interface IFontSettings
    {
        string FontColor { get; set; }
        double FontSize { get; set; }
        bool FontBold { get; set; }
        bool FontItalic { get; set; }
        bool FontUnderline { get; set; }
        string Justification { get; set; }

        object ConvertToLibrarySpecificFormat();
    }
}
