using DocGen.Abstract.Constants;
using DocGen.Abstract.Interface.Settings;
using System.Collections.Generic;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// A single subsection within the document body,
    /// with title, notes, hierarchical level, etc.
    /// </summary>
    public interface IBodySection
    {
        string Title { get; set; }
        List<string> TitleNotes { get; set; }
        List<string> SectionNotes { get; set; }
        string RichTextContent { get; set; }
        int HierarchyLevel { get; set; }
        List<IBodySection> SubBodySections { get; set; }
        IFontSettings FontSettings { get; set; }
        TitleNumberingType NumberingType { get; set; }

        void AddSubSection(IBodySection subSection);
    }
}
