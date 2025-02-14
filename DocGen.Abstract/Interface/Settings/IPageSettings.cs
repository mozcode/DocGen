namespace DocGen.Abstract.Interface.Settings
{
    /// <summary>
    /// Additional page-level settings for more advanced customization 
    /// (margins, orientation, etc.).
    /// </summary>
    public interface IPageSettings : ISettings
    {
        bool IsLandscape { get; set; }
        bool AddBackgroundImage { get; set; }
        string? BackgroundImagePath { get; set; }

        // Örnek ek alanlar:
        double MarginLeft { get; set; }
        double MarginRight { get; set; }
        double MarginTop { get; set; }
        double MarginBottom { get; set; }
    }
}
