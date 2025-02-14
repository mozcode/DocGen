using System;
using System.IO;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Concrete;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using DocGen.Pdf.Utility;
using DocGen.Abstract.Interface.Content.Table;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Geom;
using DocGen.Abstract.Application.Formatter;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

namespace DocGen.Pdf.Creator
{
    public class PdfDocumentCreator : IDocumentCreator
    {
        public void CreateDocument(IDocumentContent documentContent)
        {
            CreateDocument(documentContent, new DocumentCreateOptions());
        }

        public void CreateDocument(IDocumentContent documentContent, DocumentCreateOptions options)
        {
            // PDF çıktısını kaydetme yolu (örnek)
            string outputPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "CompleteSamplePdf.pdf");

            var writerProperties = new WriterProperties();
            using var pdfWriter = new PdfWriter(outputPath, writerProperties);
            using var pdfDoc = new PdfDocument(pdfWriter);

            if (options.IsLandscape)
            {
                pdfDoc.SetDefaultPageSize(PageSize.A4.Rotate());
            }
            else
            {
                pdfDoc.SetDefaultPageSize(PageSize.A4);
            }

            using var doc = new Document(pdfDoc);

            // Arka plan resmi ekleme
            if (options.AddBackgroundImage && !string.IsNullOrEmpty(options.BackgroundImagePath))
            {
                var imgData = ImageDataFactory.Create(options.BackgroundImagePath);
                var img = new Image(imgData).SetFixedPosition(0, 0).SetAutoScale(true);
                doc.Add(img);
            }

            // Mevcut: basit tablo render'ı
            RenderSubContent(doc, documentContent.MetaHeaderContent);
            RenderBodyContent(doc, documentContent.BodyContent);
            RenderSubContent(doc, documentContent.MetaFooterContent);
            RenderSignatureContent(doc, documentContent.MetaSignatureContent);

            // Alternatif: RichTextParser ile tam body render 
            // var parser = new RichTextParser();
            // var renderer = new Pdf.Renderer.PdfDocumentRenderer(documentContent, parser);
            // renderer.RenderAllSections(doc);

            // PDF kaydedilir, doc kapatılır
        }

        private void RenderSubContent(Document doc, ISubContent subContent)
        {
            if (subContent == null || subContent.TablesRows == null) return;

            foreach (var row in subContent.TablesRows)
            {
                if (row.TableCells == null || row.TableCells.Count == 0) continue;

                int colCount = row.TableCells.Count;
                var table = new Table(colCount).SetWidth(UnitValue.CreatePercentValue(100));

                row.ValidateColumnWidths();
                var widths = row.ColumnWidths;
                // (iText7'de tablo genişliklerini ayarlamak istersek ek ayarlar yapabiliriz.)

                foreach (var cell in row.TableCells)
                {
                    var pdfCell = RenderPdfCell(cell);
                    table.AddCell(pdfCell);
                }
                doc.Add(table);
            }
        }

        private void RenderBodyContent(Document doc, BodyContent bodyContent)
        {
            if (bodyContent == null) return;

            foreach (var section in bodyContent.BodySections)
            {
                // Başlık
                var headingPara = RenderParagraph(
                    section.Title,
                    isBold: true,
                    isItalic: false,
                    isUnderline: false,
                    colorHex: "#000000",
                    fontSize: GetFontSizeByLevel(section.HierarchyLevel));

                doc.Add(headingPara);

                // TitleNotes
                if (section.TitleNotes != null)
                {
                    foreach (var note in section.TitleNotes)
                    {
                        var notePara = RenderParagraph(note, false, true, false, "#444444", 10f);
                        doc.Add(notePara);
                    }
                }

                // İçerik
                var bodyPara = RenderParagraph(
                    section.RichTextContent,
                    section.FontSettings.FontBold,
                    section.FontSettings.FontItalic,
                    section.FontSettings.FontUnderline,
                    section.FontSettings.FontColor,
                    (float)section.FontSettings.FontSize,
                    section.FontSettings.Justification);
                doc.Add(bodyPara);

                // SectionNotes
                if (section.SectionNotes != null)
                {
                    foreach (var note in section.SectionNotes)
                    {
                        var notePara = RenderParagraph(note, false, true, false, "#777777", 9f);
                        doc.Add(notePara);
                    }
                }
            }
        }

        private void RenderSignatureContent(Document doc, ISubContent signatureContent)
        {
            if (signatureContent == null) return;

            // Birkaç boş satır
            doc.Add(new Paragraph("\n\n"));

            foreach (var row in signatureContent.TablesRows)
            {
                if (row.TableCells.Count == 0) continue;

                int colCount = row.TableCells.Count;
                var table = new Table(colCount).SetWidth(UnitValue.CreatePercentValue(100));

                row.ValidateColumnWidths();
                var widths = row.ColumnWidths;

                foreach (var cell in row.TableCells)
                {
                    var pdfCell = RenderPdfCell(cell);
                    table.AddCell(pdfCell);
                }

                doc.Add(table);
            }
        }

        private Cell RenderPdfCell(ITableCell cell)
        {
            if (cell is Abstract.Domain.Table.TableCellText textCell)
            {
                var cellParagraph = RenderParagraph(
                    textCell.Content,
                    textCell.FontSettings?.FontBold == true,
                    textCell.FontSettings?.FontItalic == true,
                    textCell.FontSettings?.FontUnderline == true,
                    textCell.FontSettings?.FontColor ?? "#000000",
                    (float)(textCell.FontSettings?.FontSize ?? 12),
                    textCell.FontSettings?.Justification ?? "left"
                );

                var pdfCell = new Cell()
                    .Add(cellParagraph)
                    .SetBorder(Border.NO_BORDER);
                return pdfCell;
            }
            else
            {
                // Desteklenmeyen hücre tipi
                var pdfCell = new Cell()
                    .Add(new Paragraph("Unsupported Cell Type"))
                    .SetBorder(Border.NO_BORDER);
                return pdfCell;
            }
        }

        private Paragraph RenderParagraph(
            string text,
            bool isBold,
            bool isItalic,
            bool isUnderline,
            string colorHex,
            float fontSize,
            string justification = "left")
        {
            string baseFont = StandardFonts.HELVETICA;
            if (isBold && isItalic) baseFont = StandardFonts.HELVETICA_BOLDOBLIQUE;
            else if (isBold) baseFont = StandardFonts.HELVETICA_BOLD;
            else if (isItalic) baseFont = StandardFonts.HELVETICA_OBLIQUE;

            var pdfFont = PdfFontFactory.CreateFont(baseFont);

            var paragraph = new Paragraph(text);
            paragraph.SetFont(pdfFont);
            paragraph.SetFontSize(fontSize);

            var rgbColor = PdfHelper.ConvertHexToColor(colorHex);
            paragraph.SetFontColor(rgbColor);

            if (isUnderline) paragraph.SetUnderline();

            var align = PdfHelper.ConvertJustification(justification);
            paragraph.SetTextAlignment(align);

            return paragraph;
        }

        private float GetFontSizeByLevel(int level)
        {
            return level switch
            {
                1 => 18f,
                2 => 16f,
                3 => 14f,
                _ => 12f
            };
        }
    }
}
