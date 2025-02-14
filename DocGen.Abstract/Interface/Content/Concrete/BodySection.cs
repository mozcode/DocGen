using System.Collections.Generic;
using DocGen.Abstract.Constants;
using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Interface.Settings;
using DocGen.Abstract.Interface.Content;

namespace DocGen.Abstract.Interface.Content.Concrete
{
    /// <summary>
    /// Represents a subsection within the document body, 
    /// with a title, notes, and hierarchical level.
    /// Validation is handled externally by FluentValidation.
    /// </summary>
    public class BodySection : IBodySection
    {
        private string _title;
        private List<string> _titleNotes;
        private List<string> _sectionNotes;
        private string _richTextContent;
        private int _hierarchyLevel;
        private List<IBodySection> _subBodySections;
        private IFontSettings _fontSettings;
        private TitleNumberingType _numberingType;

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public List<string> TitleNotes
        {
            get => _titleNotes;
            set => _titleNotes = value;
        }

        public List<string> SectionNotes
        {
            get => _sectionNotes;
            set => _sectionNotes = value;
        }

        public string RichTextContent
        {
            get => _richTextContent;
            set => _richTextContent = value;
        }

        public int HierarchyLevel
        {
            get => _hierarchyLevel;
            set => _hierarchyLevel = value;
        }

        public List<IBodySection> SubBodySections
        {
            get => _subBodySections;
            set => _subBodySections = value;
        }

        public IFontSettings FontSettings
        {
            get => _fontSettings;
            set => _fontSettings = value;
        }

        public TitleNumberingType NumberingType
        {
            get => _numberingType;
            set => _numberingType = value;
        }

        public BodySection(
            IFontSettingsFactory fontSettingsFactory,
            string title,
            List<string> titleNotes,
            List<string> sectionNotes,
            string richTextContent,
            int hierarchyLevel,
            List<IBodySection> subBodySections,
            TitleNumberingType numberingType,
            IFontSettings? fontSettings = null)
        {
            _title = title;
            _titleNotes = titleNotes;
            _sectionNotes = sectionNotes;
            _richTextContent = richTextContent;
            _hierarchyLevel = hierarchyLevel;
            _subBodySections = subBodySections;
            _numberingType = numberingType;
            _fontSettings = fontSettings ?? fontSettingsFactory.CreateFontSettings();
        }

        public void AddSubSection(IBodySection subBodySection)
        {
            _subBodySections.Add(subBodySection);
        }
    }
}
