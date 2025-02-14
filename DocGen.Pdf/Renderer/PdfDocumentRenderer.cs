using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Application.Formatter;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using DocGen.Abstract.Domain.RichText;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

namespace DocGen.Pdf.Renderer
{
    /// <summary>
    /// Handles PDF rendering of document sections 
    /// (header, body, footer, signature) via iText.
    /// 
    /// Bu sınıf, "DocumentCreator" yerine daha soyut bir "Renderer" yaklaşımı sunar.
    /// "PdfDocumentCreator" da bu sınıfı kullanarak iş yapabilir.
    /// </summary>
    public class PdfDocumentRenderer
    {
        private readonly IDocumentContent _docContent;
        private readonly RichTextParser _richTextParser;

        public PdfDocumentRenderer(IDocumentContent docContent, RichTextParser richTextParser)
        {
            _docContent = docContent;
            _richTextParser = richTextParser;
        }

        public void RenderAllSections(Document doc)
        {
            RenderHeader(doc);
            RenderBody(doc);
            RenderFooter(doc);
            RenderSignature(doc);
        }

        public void RenderHeader(Document doc)
        {
            var header = _docContent.MetaHeaderContent;
            if (header == null) return;

            // Örnek minimal ekleme.
            // Normalde tablo çizimi, satır/sütun ekleme vb. 
            // CreateDocument içinde "RenderSubContent" gibi bir metotla da yapılabilir.
            // doc.Add(new Paragraph("Header..."));
        }

        public void RenderBody(Document doc)
        {
            var body = _docContent.BodyContent;
            if (body == null) return;

            foreach (var section in body.BodySections)
            {
                var parsedElements = _richTextParser.ParseRichText(section.RichTextContent);

                var paragraph = new Paragraph();
                foreach (var pe in parsedElements)
                {
                    var txt = new Text(pe.Text);

                    // iText 7'de extension metodları set edebilir
                    // Örneğin .SetBold() vs. ama yoksa property ayarları:
                    // if (pe.IsBold) txt.SetProperty(Property.BOLD, true);
                    // if (pe.IsItalic) txt.SetProperty(Property.ITALIC, true);
                    if (pe.IsUnderline) txt.SetProperty(Property.UNDERLINE, true);

                    paragraph.Add(txt);
                }

                // section.FontSettings'e göre justifiy, color, fontSize vs.
                // Basit örnek:
                if (section.FontSettings != null)
                {
                    paragraph.SetFontColor(Pdf.Utility.PdfHelper.ConvertHexToColor(section.FontSettings.FontColor));
                    paragraph.SetFontSize((float)section.FontSettings.FontSize);

                    string baseFont = StandardFonts.HELVETICA;
                    if (section.FontSettings.FontBold && section.FontSettings.FontItalic)
                        baseFont = StandardFonts.HELVETICA_BOLDOBLIQUE;
                    else if (section.FontSettings.FontBold)
                        baseFont = StandardFonts.HELVETICA_BOLD;
                    else if (section.FontSettings.FontItalic)
                        baseFont = StandardFonts.HELVETICA_OBLIQUE;
                    var pdfFont = PdfFontFactory.CreateFont(baseFont);
                    paragraph.SetFont(pdfFont);

                    var alignment = Pdf.Utility.PdfHelper.ConvertJustification(section.FontSettings.Justification);
                    paragraph.SetTextAlignment(alignment);
                }

                doc.Add(paragraph);
            }
        }

        public void RenderFooter(Document doc)
        {
            var footer = _docContent.MetaFooterContent;
            if (footer == null) return;

            // doc.Add(new Paragraph("Footer..."));
        }

        public void RenderSignature(Document doc)
        {
            var signature = _docContent.MetaSignatureContent;
            if (signature == null) return;

            // doc.Add(new Paragraph("Signature..."));
        }
    }
}
