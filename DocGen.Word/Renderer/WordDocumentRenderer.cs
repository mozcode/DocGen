using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Domain.Table;
using DocGen.Abstract.Application.Formatter;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocGen.Word.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace DocGen.Word.Renderer
{
    /// <summary>
    /// Handles rendering of header, body, footer, signature 
    /// into an OpenXML WordprocessingDocument.
    /// </summary>
    public class WordDocumentRenderer
    {
        private readonly IDocumentContent _docContent;
        private readonly RichTextParser _richTextParser;

        public WordDocumentRenderer(IDocumentContent docContent, RichTextParser parser)
        {
            _docContent = docContent;
            _richTextParser = parser;
        }

        public void RenderAllSections(MainDocumentPart mainPart)
        {
            RenderHeader(mainPart);
            RenderBody(mainPart);
            RenderFooter(mainPart);
            RenderSignature(mainPart);
        }

        public void RenderHeader(MainDocumentPart mainPart)
        {
            var header = _docContent.MetaHeaderContent;
            if (header == null) return;

            // OpenXML ile HeaderPart oluşturulabilir.
            // Bu örnekte basitçe Body'ye ekleyeceğiz.
            var docBody = mainPart.Document.Body;
            docBody.AppendChild(new Paragraph(new Run(new Text("Header..."))));
        }

        public void RenderBody(MainDocumentPart mainPart)
        {
            var body = _docContent.BodyContent;
            if (body == null) return;

            var docBody = mainPart.Document.Body;

            // Basit örnek: Her BodySection'ı parse et ve paragraf ekle.
            foreach (var section in body.BodySections)
            {
                var parsedElements = _richTextParser.ParseRichText(section.RichTextContent);

                var paragraph = new Paragraph();
                var run = new Run();

                // Stil vb. runProperties
                // Farklı yaklaşımla "CreateStyledRun" da yapabiliriz.
                foreach (var pe in parsedElements)
                {
                    var txt = new Text(pe.Text);

                    if (pe.IsBold) run.AppendChild(new Bold());
                    if (pe.IsItalic) run.AppendChild(new Italic());
                    if (pe.IsUnderline) run.AppendChild(new Underline { Val = UnderlineValues.Single });

                    run.AppendChild(txt);
                }

                paragraph.AppendChild(run);
                docBody.AppendChild(paragraph);
            }
        }

        public void RenderFooter(MainDocumentPart mainPart)
        {
            var footer = _docContent.MetaFooterContent;
            if (footer == null) return;
            mainPart.Document.Body.AppendChild(new Paragraph(new Run(new Text("Footer..."))));
        }

        public void RenderSignature(MainDocumentPart mainPart)
        {
            var signature = _docContent.MetaSignatureContent;
            if (signature == null) return;
            mainPart.Document.Body.AppendChild(new Paragraph(new Run(new Text("Signature..."))));
        }
    }
}
