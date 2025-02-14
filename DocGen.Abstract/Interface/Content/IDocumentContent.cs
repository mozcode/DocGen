using DocGen.Abstract.Domain.Content;
using DocGen.Abstract.Interface.Content.Concrete;
using DocGen.Abstract.Interface.Settings;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Represents the overall document with body, header, footer, signature, 
    /// and a default font setting.
    /// </summary>
    public interface IDocumentContent
    {
        BodyContent BodyContent { get; }
        MetaHeaderContent MetaHeaderContent { get; }
        MetaFooterContent MetaFooterContent { get; }
        MetaSignatureContent MetaSignatureContent { get; }
        IFontSettings DefaultFontSettings { get; }

        /// <summary>
        /// Returns merged font settings by combining override and default.
        /// </summary>
        IFontSettings GetMergedFontSettings(IFontSettings? overrideSettings);
    }
}
