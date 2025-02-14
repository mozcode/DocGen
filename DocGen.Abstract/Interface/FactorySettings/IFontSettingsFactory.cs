using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Interface.FactorySettings
{
    /// <summary>
    /// Factory interface for producing IFontSettings objects.
    /// </summary>
    public interface IFontSettingsFactory
    {
        IFontSettings CreateFontSettings();
    }
}
