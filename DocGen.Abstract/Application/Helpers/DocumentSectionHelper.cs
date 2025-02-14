using System;
using System.Collections.Generic;
using DocGen.Abstract.Interface.Providers;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Domain.Content;
using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Interface.FactorySettings;

namespace DocGen.Abstract.Application.Helpers
{
    /// <summary>
    /// Example helper class that demonstrates 
    /// how to access sections via IDocumentInfoProvider,
    /// and do something useful with them.
    /// </summary>
    public class DocumentSectionHelper
    {
        private readonly IDocumentInfoProvider _docInfo;
        private readonly IFontSettingsFactory _fontSettingsFactory;

        public DocumentSectionHelper(IDocumentInfoProvider docInfoProvider, IFontSettingsFactory fontSettingsFactory)
        {
            _docInfo = docInfoProvider;
            _fontSettingsFactory = fontSettingsFactory;
        }

        /// <summary>
        /// E.g. add a row to the header with "Logo" text.
        /// </summary>
        public void DoSomethingWithHeader()
        {
            var header = _docInfo.GetHeader();
            if (header is MetaHeaderContent metaHeader)
            {
                var newRow = new TablesRow();
                newRow.ColumnWidths = new List<int> { 100 };
                var textCell = new TableCellText
                {
                    Content = "LOGO or Header Info"
                };
                newRow.TableCells.Add(textCell);

                metaHeader.TablesRows.Add(newRow);
                Console.WriteLine("Added a new row to header with 'LOGO or Header Info'.");
            }
        }

        /// <summary>
        /// E.g. add the current date/time to the footer as a separate row.
        /// </summary>
        public void DoSomethingWithFooter()
        {
            var footer = _docInfo.GetFooter();
            if (footer is MetaFooterContent metaFooter)
            {
                var newRow = new TablesRow();
                newRow.ColumnWidths = new List<int> { 100 };
                var textCell = new TableCellText
                {
                    Content = $"Page 1 - {DateTime.Now.ToShortDateString()}"
                };
                newRow.TableCells.Add(textCell);

                metaFooter.TablesRows.Add(newRow);
                Console.WriteLine("Added date/time info to footer.");
            }
        }

        /// <summary>
        /// E.g. read all BodySections, print them out, or add a new sub-section
        /// </summary>
        public void DoSomethingWithBody()
        {
            var body = _docInfo.GetBody();
            if (body is BodyContent bodyContent)
            {
                foreach (var section in bodyContent.BodySections)
                {
                    Console.WriteLine($"Found BodySection: '{section.Title}' [Level: {section.HierarchyLevel}]");
                }

                var newSection = new BodySection(
                    fontSettingsFactory: _fontSettingsFactory,
                    title: "Ek Bilgi",
                    titleNotes: new List<string>(),
                    sectionNotes: new List<string>(),
                    richTextContent: "Buraya ek paragraf bilgisi.",
                    hierarchyLevel: 2,
                    subBodySections: new List<IBodySection>(),
                    numberingType: Abstract.Constants.TitleNumberingType.Numeric
                );
                bodyContent.BodySections.Add(newSection);
                Console.WriteLine("Added a new BodySection titled 'Ek Bilgi'.");
            }
        }
    }
}
