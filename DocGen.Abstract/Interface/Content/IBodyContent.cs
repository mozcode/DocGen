using DocGen.Abstract.Interface.Settings;
using System.Collections.Generic;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Represents the body portion of a document, including sections 
    /// and default text/header font settings.
    /// </summary>
    public interface IBodyContent
    {
        List<IBodySection> BodySections { get; set; }
        IFontSettings TextFontSettings { get; set; }
        IFontSettings HeaderFontSettings { get; set; }

        void AddBodySection(IBodySection bodySection);
    }
}
